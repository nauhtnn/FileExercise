using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileExercise
{
    public class FolderModelFactory
    {
        static Random random = new Random();
        public static FolderModel Generate(string name, int maxChildren, int minChildren, int level, int maxLevel)
        {
            FolderModel folder = new FolderModel();
            folder.Name = (name == null) ? GenerateName(8) : name;
            int n = random.Next(minChildren, maxChildren);
            if(level < maxLevel)
            {
                while (0 < n)
                {
                    folder.Children.Add(Generate(null, maxChildren, minChildren, level + 1, maxLevel));
                    --n;
                }
            }
            
            return folder;
        }

        static string GenerateName(int maxLen)
        {
            int n = random.Next(maxLen + 1);
            if (n < 1)
                n = 1;
            StringBuilder s = new StringBuilder();
            while(0 < n)
            {
                int c = random.Next(26);
                if (random.Next(2) < 1)
                    s.Append((char)('a' + c));
                else
                    s.Append((char)('A' + c));
                --n;
            }
            return s.ToString();
        }
    }
}
