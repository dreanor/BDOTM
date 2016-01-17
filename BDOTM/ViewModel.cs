using helper.mvvm.baseclasses;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace BDOTM
{
    public class ViewModel : ViewModelBase<IViewModel>, IViewModel
    {
        private string jsonData;
        private bool preparationComplete;
        private const string CustomizationData = "CustomizationData";
        private string myDocs;

        public ViewModel()
        {
            Templates = new ObservableCollection<TemplateItem>();
            PrepareData();
            LoadData();
            preparationComplete = true;
        }

        public ObservableCollection<TemplateItem> Templates
        {
            get { return this.Get(x => x.Templates); }
            set { this.Set(x => x.Templates, value); }
        }

        private void Template_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (preparationComplete)
            {
                if (e.PropertyName == GetPropertyName(x => x.Templates[0].IsActive))
                {
                    var a = (TemplateItem)sender;
                    var newActiveFile = Templates.Where(x => x.Path == a.Path).FirstOrDefault();
                    if (newActiveFile != null && a.IsActive)
                    {
                        var oldActiveFile = Templates.Where(x => x.Name == CustomizationData).FirstOrDefault();
                        oldActiveFile.Name += Guid.NewGuid();

                        newActiveFile.Name = CustomizationData;

                        var pathO = myDocs + @"\" + oldActiveFile.Name;
                        var pathN = myDocs + @"\" + newActiveFile.Name;

                        File.Move(oldActiveFile.Path, pathO);
                        File.Move(newActiveFile.Path, pathN);

                        oldActiveFile.Path = pathO;
                        newActiveFile.Path = pathN;
                    }
                }
                SaveData();
            }
        }

        private void PrepareData()
        {
            string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string settingsPath = appData + @"\BlackDesertOnlineTemplateManager";
            jsonData = settingsPath + @"\Data.json";

            Directory.CreateDirectory(settingsPath);
            if (!File.Exists(jsonData))
            {
                File.Create(jsonData).Close();
            }
        }

        private void LoadData()
        {
            myDocs = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Black Desert\Customization";

            foreach (var item in Directory.GetFiles(myDocs))
            {
                var template = new TemplateItem(Path.GetFileName(item), "", item);
                template.PropertyChanged += Template_PropertyChanged;
                if (template.Name == CustomizationData)
                {
                    template.IsActive = true;
                }
                Templates.Add(template);
            }

            using (StreamReader reader = new StreamReader(jsonData))
            {
                var data = JsonConvert.DeserializeObject<List<TemplateItem>>(reader.ReadToEnd());
                if (data != null)
                {
                    foreach (var item in data)
                    {
                        var found = Templates.Where(x => x.Path == item.Path).FirstOrDefault();
                        if (found != null)
                        {
                            found.Name = item.Name;
                            found.Description = item.Description;
                        }
                    }
                }
            }
        }

        private void SaveData()
        {
            JsonSerializer serializer = new JsonSerializer();

            using (StreamWriter sw = new StreamWriter(jsonData))
            using (JsonWriter jsonWriter = new JsonTextWriter(sw))
            {
                jsonWriter.Formatting = Formatting.Indented;
                var data = JsonConvert.SerializeObject(Templates);
                serializer.Serialize(jsonWriter, Templates);
            }
        }
    }
}