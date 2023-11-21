using System;
using System.CommandLine;

namespace AzureDevOpsInit;

public abstract class CommandException : Exception
{
    public CommandException(string message) : base(message) { }
    public abstract int GetExitCode();
}

public class InputException : CommandException
{
    public InputException(string message) : base(message) { }
    public override int GetExitCode() => 1;
}

public class ValidationException : CommandException
{
    public ValidationException(string message) : base(message) { }
    public override int GetExitCode() => 2;
}