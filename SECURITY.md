# Security Policy

## Supported Versions

Only the latest stable release published on [NuGet](https://www.nuget.org/packages/tetri.net.CalendarVersioning/) receives security updates.

| Version | Supported          |
| ------- | ------------------ |
| Latest  | ✅                 |
| < Latest| ❌                 |

## Reporting a Vulnerability

If you discover a security vulnerability, please open an [issue](https://github.com/tetri/CalendarVersioning/issues) with the label `security`. Do not disclose the vulnerability publicly until it has been addressed.

You can expect an initial response within 5 business days. Once triaged, we will work on a fix and publish a new patch version as soon as possible.

## Security Measures

- **Input validation:** All `Parse` methods validate input length (max 256 characters) and limit the number of components to prevent DoS via excessive memory allocation.
- **Overflow protection:** `TryParse` catches `OverflowException` to safely handle out-of-range numeric values.
- **Immutability:** `CalendarVersion` instances are immutable, preventing accidental or malicious state modification after creation.
