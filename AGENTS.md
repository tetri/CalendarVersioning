# AI Agent Guide for CalendarVersioning

Welcome to the CalendarVersioning repository! This document contains essential information and guidelines to help AI agents (like you) work effectively within this codebase.

## Project Overview

This is a robust [Calendar Versioning](https://calver.org/) implementation for .NET with full support for parsing, comparison, and format customization.

- **Language:** C# 11.0
- **Framework:** .NET 9.0 / .NET 10.0
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

<!-- trail:start -->
## Trail — repo context graph

This repo is indexed in `.trail/cache/`: small linked markdown nodes that explain each
system and carry exact file:line spans, kept in sync with the code through git.

For ANY task here — understanding how something works, finding where code lives,
or scoping a change — get context from the graph before grepping or opening
source files. Re-ask freely (it's cheap) and reuse literal identifiers you
already have (symbol, error string, file name) as the query. New to this repo?
Run `trail map` first — a token-budgeted orientation (dir clusters, hubs,
hotspots), no LLM, no key.

- Run `trail ask "<your question>" --source` → ranked nodes with the relevant
  code spans inlined (each hit's ≤8-line crux by default; `--full` for whole
  definitions when the crux isn't enough). Match the tool to the task shape:
  for understanding or editing, the top node IS the answer — cite its
  `covers:` file:line spans and edit straight from `--source`. For
  exhaustive tasks ("every occurrence / every caller of this pattern"), ranked
  results are top-N, not complete — run `trail grep "<literal>"` instead
  (exhaustive over indexed files, grouped by enclosing symbol), falling back
  to raw `grep -rn` only for unindexed files.
- `trail skeleton <file>` → every definition's signature + span, ~10× cheaper
  than reading the file; use it to skim an API surface.
- `trail callers <symbol>` gives precomputed, exact edges — who calls this.
  Add `--direction out` for what it calls, or `--depth N` to walk
  transitively for the full blast radius. For structural questions, skip
  ranking and use this directly.
- Or browse: `.trail/cache/INDEX.md` lists every node; follow the links.
- Monorepos and folders of multiple repos rank fairly across sub-projects —
  hits carry `[scope/]` labels naming which one they're from. Narrow with
  `trail ask "<task>" --in <scope>/` once you know where you're working.

If a returned span is truncated ("+N more lines"), open the file at that exact
range before finalizing. Only open source files when a node genuinely lacks a
needed detail, and then at the exact file:line the node points to — never
re-read whole files.

After big code changes, refresh the graph with `trail build` (deterministic,
no API key, $0).
<!-- trail:end -->
