# Palindrome Checker (Recursivo)

```csharp
public bool CheckPalindrome(string value)
{
    if (string.IsNullOrEmpty(value)) return true;
    return IsPalindrome(value, 0, value.Length - 1);
}

private bool IsPalindrome(string value, int left, int right)
{
    if (left >= right) return true;
    if (char.ToLower(value[left]) != char.ToLower(value[right])) return false;
    return IsPalindrome(value, left + 1, right - 1);
}
```

**Explicação:**
A função compara os caracteres das extremidades e avança para o centro recursivamente. Se todos forem iguais, é palíndromo. Complexidade O(n). 
