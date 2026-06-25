# CalendarVersioning

[![NuGet Version](https://img.shields.io/nuget/v/tetri.net.CalendarVersioning.svg?style=flat-square&logo=nuget)](https://www.nuget.org/packages/tetri.net.CalendarVersioning/)
[![NuGet Downloads](https://img.shields.io/nuget/dt/tetri.net.CalendarVersioning.svg?style=flat-square&logo=nuget)](https://www.nuget.org/packages/tetri.net.CalendarVersioning/)
[![License](https://img.shields.io/github/license/tetri/CalendarVersioning.svg?style=flat-square&logo=github)](LICENSE)
[![Github Build Status](https://img.shields.io/github/actions/workflow/status/tetri/CalendarVersioning/publish.yml?style=flat-square&logo=github)](https://github.com/tetri/CalendarVersioning/actions)
[![AppVeyor Build Status](https://img.shields.io/appveyor/build/tetri/calendarversioning?style=flat-square&logo=appveyor)](https://ci.appveyor.com/project/tetri/calendarversioning)

Uma implementação robusta de [Calendar Versioning (CalVer)](https://calver.org/) para .NET com suporte completo a parsing, comparação e formatação personalizada.

## 📦 Instalação

Instale via NuGet Package Manager:

```bash
dotnet add package tetri.net.CalendarVersioning
```

Ou adicione diretamente ao seu `.csproj`:

```xml
<PackageReference Include="tetri.net.CalendarVersioning" Version="0.0.4" />
```

## 🚀 Início Rápido

### Criando versões
```csharp
// A partir de uma string
var version = CalendarVersion.Parse("2025.04.29");

// Com formato personalizado (YY.MM.Minor)
var format = new CalendarVersionFormat("YY.MM.Minor");
var custom = CalendarVersion.Parse("25.04.1", format);

// Usando o construtor
var version = new CalendarVersion(year: 2025, month: 4, day: 29, minor: 1);
```

### Comparando versões
```csharp
var v1 = CalendarVersion.Parse("2025.04");
var v2 = CalendarVersion.Parse("2025.05");

if (v1 < v2) 
{
    Console.WriteLine($"{v1} é anterior a {v2}");
}
```

### Parsing seguro (TryParse)
```csharp
if (CalendarVersion.TryParse("2025.04.29", out var version))
{
    Console.WriteLine($"Parseado: {version}");
}

// Com formato personalizado
var format = new CalendarVersionFormat("YY.MM.DD");
if (CalendarVersion.TryParse("25.04.29", format, out var custom))
{
    Console.WriteLine($"Parseado: {custom}");
}
```

### Operações suportadas
```csharp
// Igualdade
bool equal = v1 == v2; 

// Comparação
bool greater = v1 > v2;

// Métodos de comparação
int result = v1.CompareTo(v2);
```

## ✨ Funcionalidades

✅ Parsing estrito de versões Calendar com validação de formato  
✅ Parsing seguro via `TryParse` (sem exceções em entradas inválidas)  
✅ Proteção DoS: máximo de 256 caracteres, número limitado de componentes  
✅ Suporte completo a comparação de versões  
✅ Formatos personalizados (`YYYY.MM`, `YY.MM.DD.Minor`, etc)  
✅ Operadores sobrecarregados (==, !=, <, >, <=, >=)  
✅ Imutável e thread-safe  
✅ Serialização System.Text.Json pronta (via `CalendarVersionConverter`)  
✅ Implementa `IParsable<CalendarVersion>` (.NET 7+)  

## 📚 Exemplos Avançados

### Parsing com formato personalizado
```csharp
var format = new CalendarVersionFormat("YYYY.MM.Minor");
var version = CalendarVersion.Parse("2025.04.2", format);

Console.WriteLine(version.Year);  // 2025
Console.WriteLine(version.Month); // 4
Console.WriteLine(version.Minor); // 2
```

### Comparando versões detalhadas
```csharp
var stable = CalendarVersion.Parse("2025.04.15");
var hotfix = CalendarVersion.Parse("2025.04.15.1");

Console.WriteLine(hotfix > stable); // True
```

### Serialização JSON
```csharp
// Serialização funciona diretamente via o JsonConverter embutido
var version = CalendarVersion.Parse("2025.04.29");
string json = JsonSerializer.Serialize(version);
// "2025.04.29"

// Desserialização
var deserialized = JsonSerializer.Deserialize<CalendarVersion>("\"2025.04.29\"");
```

### Limites de segurança

O método `CalendarVersion.Parse` impõe um tamanho máximo de entrada de 256 caracteres e limita o número de componentes separados por ponto para prevenir ataques de negação de serviço via alocação excessiva de memória.

```csharp
// Lança ArgumentException: entrada excede 256 caracteres
CalendarVersion.Parse(new string('a', 257));

// Lança FormatException: muitos componentes
CalendarVersion.Parse("2025.04.01.1.extra.dots");
```

## 🤝 Contribuindo

Aceitamos contribuições! Siga estes passos:

1. Faça um fork do projeto  
2. Crie sua branch de funcionalidade (`git checkout -b feature/minha-funcionalidade`)  
3. Commit suas alterações (`git commit -m 'Adiciona nova funcionalidade'`)  
4. Faça push para a branch (`git push origin feature/minha-funcionalidade`)  
5. Abra um Pull Request  

## 📄 Licença

Este projeto está licenciado sob a Licença MIT - veja o arquivo [LICENSE](LICENSE) para mais detalhes.

---

Criado com 🧠 por [Tetri Mesquita](https://tetri.net)
