using FolkerD0C.DependencyInjection;
using XmlDocMarkdown.Core;

string[] defaultDocumentationArgs = [ typeof(ServiceProvider).Assembly.GetName().Name
    ?? "FolkerD0C.DependencyInjection" ];

var doucmentationArgs = defaultDocumentationArgs.Concat(args).ToArray();

XmlDocMarkdownApp.Run(doucmentationArgs);
