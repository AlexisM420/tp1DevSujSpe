using FileScanner.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace FileScanner.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private string selectedFolder;
        private ObservableCollection<Items> folderItems = new ObservableCollection<Items>();
         
        public RelayCommand<string> OpenFolderCommand { get; private set; }
        public RelayCommand<string> ScanFolderCommand { get; private set; }

        public ObservableCollection<Items> FolderItems { 
            get => folderItems;
            set 
            { 
                folderItems = value;
                OnPropertyChanged();
            }
        }

        public string SelectedFolder
        {
            get => selectedFolder;
            set
            {
                selectedFolder = value;
                OnPropertyChanged();
                ScanFolderCommand.RaiseCanExecuteChanged();
            }
        }

        public MainViewModel()
        {
            OpenFolderCommand = new RelayCommand<string>(OpenFolder);
            ScanFolderCommand = new RelayCommand<string>(ScanFolderAsync, CanExecuteScanFolder);
        }

        private bool CanExecuteScanFolder(string obj)
        {
            return !string.IsNullOrEmpty(SelectedFolder); 
        }

        private void OpenFolder(string obj)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    SelectedFolder = fbd.SelectedPath;
                }
            }
        }

        private async void ScanFolderAsync(string dir)
        {
                
            try
            {
                await Task.Run(() => FolderItems = new ObservableCollection<Items>(GetDirFiles(dir)));
                #region
                // below = some debug shit
                //foreach (var item in Directory.EnumerateDirectories(dir, "*"))
                //{
                //    Item i = new Item();
                //    i.Name = item;
                //    Items.Add(i);
                //    Debug.WriteLine(i.Name);
                //}
                //foreach (var item in Directory.EnumerateFiles(dir, "*"))
                //{
                //    FolderItems.Add(item);
                //    Debug.WriteLine(item);
                //}
                #endregion
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.ToString());
            }


            ObservableCollection<Items> FolderItemss = new ObservableCollection<Items>();
            try
            {
                await Task.Run(() => FolderItemss = new ObservableCollection<Items>(GetDirDirs(dir)));
                #region
                // below = some debug shit
                //foreach (var item in Directory.EnumerateDirectories(dir, "*"))
                //{
                //    Item i = new Item();
                //    i.Name = item;
                //    Items.Add(i);
                //    Debug.WriteLine(i.Name);
                //}
                //foreach (var item in Directory.EnumerateFiles(dir, "*"))
                //{
                //    FolderItems.Add(item);
                //    Debug.WriteLine(item);
                //}
                #endregion
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.ToString());
            }

            int p = 0;
            do
            {
                FolderItems.Add(FolderItemss[p]);
                p++;
            } while (p < FolderItemss.Count());
        }



        // actually gets the files
        private IEnumerable<Items> GetDirFiles(string dir)
        {
            foreach (var d in Directory.EnumerateDirectories(dir, "*", SearchOption.AllDirectories))
            {
                Items i = new Items();
                i.Name = d;
                i.Img = "/Resources/folderIcon.png";
                yield return i;
            }
            #region
            //try
            //{
            //    foreach (var d in Directory.EnumerateDirectories(dir, "*"))
            //    {
            //        //yield return d;

            //        foreach (var f in Directory.EnumerateFiles(d, "*"))
            //        {
            //            //yield return f;
            //        }
            //    }
            //}
            //catch(Exception ex)
            //{
            //    Console.Write(ex);
            //    yield break;
            //}

            //foreach (var d in Directory.EnumerateDirectories(dir, "*"))
            //{
            //    Items i = new Items();
            //    i.Name = d;
            //    i.Img = "/Resources/folderIcon.png";
            //    yield return i;
            //}

            //foreach (var f in Directory.EnumerateFiles(dir, "*"))
            //{
            //    Items i = new Items();
            //    i.Name = f;
            //    i.Img = "/Resources/fileIcon.jpg";
            //    yield return i;
            //}
            #endregion
        }

        // actually gets the directories
        private IEnumerable<Items> GetDirDirs(string dir)
        {
            foreach (var d in Directory.EnumerateFiles(dir, "*", SearchOption.AllDirectories))
            {
                Items i = new Items();
                i.Name = d;
                i.Img = "/Resources/fileIcon.jpg";
                yield return i;
            }
            #region
            //try
            //{
            //    foreach (var d in Directory.EnumerateDirectories(dir, "*"))
            //    {
            //        //yield return d;

            //        foreach (var f in Directory.EnumerateFiles(d, "*"))
            //        {
            //            //yield return f;
            //        }
            //    }
            //}
            //catch(Exception ex)
            //{
            //    Console.Write(ex);
            //    yield break;
            //}

            //foreach (var d in Directory.EnumerateDirectories(dir, "*"))
            //{
            //    Items i = new Items();
            //    i.Name = d;
            //    i.Img = "/Resources/folderIcon.png";
            //    yield return i;
            //}

            //foreach (var f in Directory.EnumerateFiles(dir, "*"))
            //{
            //    Items i = new Items();
            //    i.Name = f;
            //    i.Img = "/Resources/fileIcon.jpg";
            //    yield return i;
            //}
            #endregion
        }

        ///TODO : Tester avec un dossier avec beaucoup de fichier
        ///TODO : Rendre l'application asynchrone
        ///TODO : Ajouter un try/catch pour les dossiers sans permission    DONE

    }

    public class Items
    {
        public string Name { get; set; }
        public string Img { get; set; }
    }

}
