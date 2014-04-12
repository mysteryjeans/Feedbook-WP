using System;
using System.IO.IsolatedStorage;
using System.Runtime.Serialization.Json;
using System.Collections.ObjectModel;
using Feedbook.Phone7.Model;
using System.IO;
using System.Text;

namespace Feedbook.Phone7
{
    public static class Storage
    {
        private const string CHANNELS_KEY = "CHANNELS";

        private const string FAVORITES_CATEGORIES_KEY = "FAVORITES_CATEGORIES";

        public static ObservableCollection<Channel> Channels { get; private set; }

        public static ObservableCollection<CategoryFeed> Favorites { get; private set; }

        public static void Load()
        {
            Channels = GetObject<ObservableCollection<Channel>>(CHANNELS_KEY) ?? new ObservableCollection<Channel>();
            Favorites = GetObject<ObservableCollection<CategoryFeed>>(FAVORITES_CATEGORIES_KEY) ?? new ObservableCollection<CategoryFeed>();
        }

        public static void Save()
        {
            SaveObject<ObservableCollection<Channel>>(CHANNELS_KEY, Channels);
            SaveObject<ObservableCollection<CategoryFeed>>(FAVORITES_CATEGORIES_KEY, Favorites);
        }

        public static T GetObject<T>(string key)
        {
            if (IsolatedStorageSettings.ApplicationSettings.Contains(key))
            {
                string serializedObject = IsolatedStorageSettings.ApplicationSettings[key].ToString();
                return Deserialize<T>(serializedObject);
            }

            return default(T);
        }

        public static void SaveObject<T>(string key, T objectToSave)
        {
            string serializedObject = Serialize(objectToSave);
            IsolatedStorageSettings.ApplicationSettings[key] = serializedObject;
        }

        private static string Serialize(object objectToSerialize)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(objectToSerialize.GetType());
                serializer.WriteObject(ms, objectToSerialize);
                ms.Position = 0;

                using (StreamReader reader = new StreamReader(ms))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        private static T Deserialize<T>(string jsonString)
        {
            using (MemoryStream ms = new MemoryStream(Encoding.Unicode.GetBytes(jsonString)))
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
                return (T)serializer.ReadObject(ms);
            }
        }
    }
}
