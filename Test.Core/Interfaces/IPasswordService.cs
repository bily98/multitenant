namespace Test.Core.Interfaces
{
    public interface IPasswordService
    {
        /// <summary>
        /// Checks if the password matches the hash
        /// </summary>
        /// <param name="hash">The hash to check against</param>
        /// <param name="password">The password to check</param>
        /// <returns>Returns true if the password matches the hash, false otherwise</returns>
        bool Check(string hash, string password);

        /// <summary>
        /// Hashes the password
        /// </summary>
        /// <param name="password">The password to hash</param>
        /// <returns>Returns the hashed password</returns>
        string Hash(string password);
    }
}
