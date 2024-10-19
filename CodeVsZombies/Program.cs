// See https://aka.ms/new-console-template for more information

// If "inputPath" console arg was provided, read input from inputPath, else read from console
// See launchsettings.json
IInputProvider inputProvider = args.FirstOrDefault() is var inputPath && inputPath is not null
                                   ? new FileInputProvider(inputPath)
                                   : new ConsoleInputProvider();
                                   
