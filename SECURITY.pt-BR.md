# Política de Segurança

## Versões Suportadas

Apenas a versão estável mais recente publicada no [NuGet](https://www.nuget.org/packages/tetri.net.CalendarVersioning/) recebe atualizações de segurança.

| Versão | Suportada          |
| ------ | ------------------ |
| Última | ✅                 |
| Anteriores | ❌             |

## Reportando uma Vulnerabilidade

Se você descobrir uma vulnerabilidade de segurança, por favor abra uma [issue](https://github.com/tetri/CalendarVersioning/issues) com a label `security`. Não divulgue a vulnerabilidade publicamente até que ela seja resolvida.

Você pode esperar uma resposta inicial em até 5 dias úteis. Após a triagem, trabalharemos em uma correção e publicaremos uma nova versão o mais rápido possível.

## Medidas de Segurança

- **Validação de entrada:** Todos os métodos `Parse` validam o tamanho da entrada (máximo de 256 caracteres) e limitam o número de componentes para prevenir DoS via alocação excessiva de memória.
- **Proteção contra overflow:** `TryParse` captura `OverflowException` para tratar com segurança valores numéricos fora dos limites.
- **Imutabilidade:** Instâncias de `CalendarVersion` são imutáveis, prevenindo modificação acidental ou maliciosa do estado após a criação.
