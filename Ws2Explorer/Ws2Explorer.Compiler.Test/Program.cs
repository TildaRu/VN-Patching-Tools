﻿using System.Reflection;
using Ws2Explorer;
using Ws2Explorer.Compiler;

if (args.Length == 0) {
    var thisPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
    args = new[] { Path.Combine(thisPath!, "TestData") };
}

int total = 0;
int failed = 0;

var root = await Folder.GetFolder(args[0], CancellationToken.None, null);
foreach (var arcMeta in root.ListChildren()) {
    if (!arcMeta.Name.EndsWith(".arc", StringComparison.OrdinalIgnoreCase)) {
        continue;
    }
    var arcFile = await root.GetChild(arcMeta.Name, CancellationToken.None, null);
    if (arcFile is not IFolder arcFolder) {
        continue;
    }
    await Console.Out.WriteLineAsync($"[{arcFolder.FullPath}]");

    foreach (var ws2Meta in arcFolder.ListChildren()) {
        if (!ws2Meta.Name.EndsWith(".ws2", StringComparison.OrdinalIgnoreCase)) {
            continue;
        }
        var ws2 = await arcFolder.GetChild(ws2Meta.Name, CancellationToken.None, null);
        if (ws2 is not BinaryFile ws2File) {
            continue;
        }
        await Console.Out.WriteLineAsync($"  {arcFolder.FullPath}/{ws2File.Name}");

        try {
            ws2File.Stream.Reset();
            var opcodes = Ws2Compiler.Decompile(ws2File.Stream.MemoryStream, out var version, out var encrypted);
            await Console.Out.WriteLineAsync($"  version: {version}, encrypted: {encrypted}");

            var recompiled = Ws2Compiler.Compile(opcodes, version, encrypted);
            var recompiledMemoryStream = new MemoryStream();
            await recompiled.CopyToAsync(recompiledMemoryStream);

            var original = ws2File.Stream.MemoryStream.ToArray();
            if (!original.SequenceEqual(recompiledMemoryStream.ToArray())) {
                await Console.Out.WriteLineAsync("Recompilation not the same.");
                failed++;
                //await File.WriteAllBytesAsync(ws2File.Name[..^3] + "recompiled.ws2", recompiledMemoryStream.ToArray());
            }
        } catch (Exception ex) {
            await Console.Out.WriteLineAsync(ex.ToString());
            failed++;
        }
        total++;
    }
}

await Console.Out.WriteLineAsync($"{total - failed}/{total} passed.");
