# AI Agent Guide for CalendarVersioning

Welcome to the CalendarVersioning repository! This document contains essential information and guidelines to help AI agents (like you) work effectively within this codebase.

## Project Overview

This is a robust [Calendar Versioning](https://calver.org/) implementation for .NET with full support for parsing, comparison, and format customization.

- **Language:** C# 10.0
- **Framework:** .NET 9.0
- **Testing Framework:** xUnit
- **Core Library Path:** `src/CalendarVersioning/`
- **Tests Path:** `tests/CalendarVersioning.Tests/`

## Environment Setup & Commands

The sandbox might require installing or using `.dotnet` from specific paths.

If you encounter missing framework errors, make sure you are using the local `.dotnet` install:
- **Build:** `$HOME/.dotnet/dotnet build`
- **Test:** `$HOME/.dotnet/dotnet test`
- **Check outdated packages:** `$HOME/.dotnet/dotnet list package --outdated`
- **Update package:** `$HOME/.dotnet/dotnet add [Project.csproj] package [PackageName] -v [Version]`

*Note: Ensure to use `$HOME/.dotnet/dotnet` explicitly if the global `dotnet` command points to an older SDK version or lacks the target framework.*

## Coding Guidelines

1. **Immutability:** Core classes like `CalendarVersion` should be immutable. Use `init` properties or `readonly` fields.
2. **Nullable Reference Types:** Nullable reference types (`<Nullable>enable</Nullable>`) are enabled. Pay close attention to `?` and handle nulls correctly.
3. **Thread Safety:** Ensure methods (specially static parsers and comparers) are thread-safe.
4. **Validation:** Perform strict validation on parsing and object creation. `CalendarVersion` throws exceptions for invalid formats or out-of-range dates.
5. **Testing:** Practice TDD (Test-Driven Development) when applicable. Add unit tests for every new feature or bug fix. Tests are located in `tests/CalendarVersioning.Tests/UnitTests/` and categorized by behavior (e.g., `ParsingTests.cs`, `ComparisonTests.cs`). Use `xUnit` assertions (`Assert.Equal`, `Assert.Throws`, etc.).

## Key Classes

- `CalendarVersion`: The main class representing a CalVer string. It implements `IComparable<CalendarVersion>` and provides comparison operators. Contains `Parse` methods.
- `CalendarVersionFormat`: Handles custom formatting strings like "YYYY.MM.Minor".
- `CalendarVersionConverter`: Used for JSON serialization/deserialization via `System.Text.Json`.

## Best Practices for AI Agents

1. **Verify Your Work:** Always run tests (`$HOME/.dotnet/dotnet test`) after any code modification.
2. **Review Diff Carefully:** When using search-and-replace, make sure the context lines exactly match the target file.
3. **Follow Pre-Commit Steps:** Ensure all verifications, tests, and standard reviews are requested before submitting changes.
