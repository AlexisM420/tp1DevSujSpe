
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace FileScanner.Models
{
    public class StringModel : INotifyPropertyChanged
    {
            private string folderContent;
            public string FolderContent
            {
                get => folderContent;
                set
                {
                folderContent = value;
                    OnPropertyChanged();
                }
            }


            private string img;
            public string Img
            {
                get => img;
                set
                {
                    img = value;
                    OnPropertyChanged();
                }
            }

            public event PropertyChangedEventHandler PropertyChanged;
            protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        
    }

}
