using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Text;

namespace Test.Infrastructure.Extensions
{
    public static class ModelBuilderExtensions
    {
        /// <summary>
        /// This method will set to lowercase and add underscores to the table name.
        /// </summary>
        /// <param name="modelBuilder">The model builder reference.</param>
        /// <param name="preserveAcronyms">If true, acronyms will be preserved and separated by underscores.</param>
        public static void SetSimpleUnderscoreTableNameConvention(this ModelBuilder modelBuilder, bool preserveAcronyms)
        {
            foreach (IMutableEntityType entity in modelBuilder.Model.GetEntityTypes())
            {
                string underscoreRegex = AddUndercores(entity.DisplayName(), preserveAcronyms);
                entity.SetTableName(underscoreRegex.ToLower());
            }
        }

        /// <summary>
        /// This method will add underscores to the table name.
        /// </summary>
        /// <param name="tableName">The table name.</param>
        /// <param name="preserveAcronyms">The preserve acronyms.</param>
        /// <returns>Returns the table name with underscores.</returns>
        private static string AddUndercores(string tableName, bool preserveAcronyms)
        {
            if (string.IsNullOrWhiteSpace(tableName))
                return string.Empty;

            var newText = new StringBuilder(tableName.Length * 2);
            newText.Append(tableName[0]);

            for (int i = 1; i < tableName.Length; i++)
            {
                if (char.IsUpper(tableName[i]))
                    if ((tableName[i - 1] != '_' && !char.IsUpper(tableName[i - 1])) ||
                        (preserveAcronyms && char.IsUpper(tableName[i - 1]) &&
                         i < tableName.Length - 1 && !char.IsUpper(tableName[i + 1])))
                        newText.Append('_');
                newText.Append(tableName[i]);
            }
            return newText.ToString();
        }
    }
}
