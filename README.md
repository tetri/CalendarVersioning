# CalendarVersioning

[![NuGet Version](https://img.shields.io/nuget/v/tetri.net.CalendarVersioning.svg?style=flat-square&logo=nuget)](https://www.nuget.org/packages/tetri.net.CalendarVersioning/)
[![NuGet Downloads](https://img.shields.io/nuget/dt/tetri.net.CalendarVersioning.svg?style=flat-square&logo=nuget)](https://www.nuget.org/packages/tetri.net.CalendarVersioning/)
[![License](https://img.shields.io/github/license/tetri/CalendarVersioning.svg?style=flat-square&logo=github)](LICENSE)
[![Github Build Status](https://img.shields.io/github/actions/workflow/status/tetri/CalendarVersioning/publish.yml?style=flat-square&logo=github)](https://github.com/tetri/CalendarVersioning/actions)
[![AppVeyor Build Status](https://img.shields.io/appveyor/build/tetri/calendarversioning?style=flat-square&logo=appveyor)](https://ci.appveyor.com/project/tetri/calendarversioning)

A robust [Calendar Versioning](https://calver.org/) implementation for .NET with full support for parsing, comparison, and format customization.

## 📦 Installation

Install via NuGet Package Manager:

```bash
dotnet add package tetri.net.CalendarVersioning
```

Or add directly to your `.csproj`:

```xml
<PackageReference Include="tetri.net.CalendarVersioning" Version="1.0.0" />
```

## 🚀 Quick Start

### Creating versions
```csharp
// From string
var version = CalendarVersion.Parse("2025.04.29");

// With custom format (YY.MM.Minor)
var format = new CalendarVersionFormat("YY.MM.Minor");
var custom = CalendarVersion.Parse("25.04.1", format);

// Using constructor
var version = new CalendarVersion(year: 2025, month: 4, day: 29, minor: 1);
```

### Comparing versions
```csharp
var v1 = CalendarVersion.Parse("2025.04");
var v2 = CalendarVersion.Parse("2025.05");

if (v1 < v2) 
{
    Console.WriteLine($"{v1} is earlier than {v2}");
}
```

### Supported operations
```csharp
// Equality
bool equal = v1 == v2; 

// Comparison
bool greater = v1 > v2;

// Comparison methods
int result = v1.CompareTo(v2);
```

## ✨ Features

✅ Strict Calendar Version parsing with format validation  
✅ Full version comparison support  
✅ Custom formats (`YYYY.MM`, `YY.MM.DD.Minor`, etc)  
✅ Overloaded operators (==, !=, <, >, <=, >=)  
✅ Immutable and thread-safe  
✅ JSON/XML serialization ready  

## 📚 Advanced Examples

### Custom format parsing
```csharp
var format = new CalendarVersionFormat("YYYY.MM.Minor");
var version = CalendarVersion.Parse("2025.04.2", format);

Console.WriteLine(version.Year);  // 2025
Console.WriteLine(version.Month); // 4
Console.WriteLine(version.Minor); // 2
```

### Comparing detailed versions
```csharp
var stable = CalendarVersion.Parse("2025.04.15");
var hotfix = CalendarVersion.Parse("2025.04.15.1");

Console.WriteLine(hotfix > stable); // True
```

## 🤝 Contributing

We welcome contributions! Please follow these steps:

1. Fork the project  
2. Create your feature branch (`git checkout -b feature/amazing-feature`)  
3. Commit your changes (`git commit -m 'Add some amazing feature'`)  
4. Push to the branch (`git push origin feature/amazing-feature`)  
5. Open a Pull Request  

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

---

Crafted with 🧠 by [Tetri Mesquita](https://tetri.net)