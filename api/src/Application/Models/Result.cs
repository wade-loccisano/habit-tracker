﻿namespace Application.Models;

public class Result
{
    internal Result(bool succeeded, IEnumerable<string> errors)
    {
        Succeeded = succeeded;
        Errors = errors.ToArray();
    }

    public bool Succeeded { get; init; }

    public string[] Errors { get; init; }

    public static Result Success() => new Result(true, Array.Empty<string>());

    public static Result Failure(IEnumerable<string> errors) => new Result(false, errors);
}
