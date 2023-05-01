using System.Security.Cryptography;

// Constant fields
Dictionary<KeyValuePair<string, byte[]>, string> passwordHashCache = new Dictionary<KeyValuePair<string, byte[]>, string>();

byte[] bytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };
byte[] hash = new byte[20];
byte[] hashBytes = new byte[36];
Rfc2898DeriveBytes pbkdf2 = null;

// Function calls
var password = "password";

for (int i = 0; i < 200; i++)
{
    GeneratePasswordHashUsingSalt(password, bytes);
}
Console.Read();


// Main logic
string GeneratePasswordHashUsingSalt(string passwordText, byte[] salt)
{
    var iterate = 10000;
    var hashKey = new KeyValuePair<string, byte[]>(passwordText, salt);
    if (passwordHashCache.ContainsKey(hashKey)) return passwordHashCache[hashKey];

    pbkdf2 = new Rfc2898DeriveBytes(passwordText, salt, iterate);

    Buffer.BlockCopy(salt, 0, hashBytes, 0, 16);
    Buffer.BlockCopy(pbkdf2.GetBytes(20), 0, hashBytes, 16, 20);

    var passwordHash = Convert.ToBase64String(hashBytes.ToArray());

    passwordHashCache[hashKey] = passwordHash;

    return passwordHash;
}
