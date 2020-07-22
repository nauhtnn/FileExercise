using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileExercise
{
    public class FolderModel
    {
        public string Name;
        public List<FolderModel> Children;
        public FolderModel()
        {
            Children = new List<FolderModel>();
        }
    }
}
