using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Diagnostics;

namespace CoachLibrairie
{
    public class AccesDistant
    {
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _serializerOptions;

        // Propriétés avec setters privés pour protéger l'intégrité des données
        public List<Profil> Items { get; private set; }
        public Profil Item { get; private set; }

        public AccesDistant()
        {
            _client = new HttpClient();
            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
        }

        /// <summary>
        /// Envoie un nouveau profil au serveur (POST)
        /// </summary>
        public async Task AjoutProfil(Profil profil)
        {
            Uri uri = new Uri("https://pepepc.ddns.net/coachapp-db/insertprofil.php");

            try
            {
                string json = JsonSerializer.Serialize<Profil>(profil, _serializerOptions);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _client.PostAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"\tNew Profil inserted.");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
        }

        /// <summary>
        /// Récupère le dernier profil enregistré (GET)
        /// </summary>
        public async Task<Profil> RecupDernierProfil()
        {
            Item = new Profil();
            Uri uri = new Uri("https://pepepc.ddns.net/coachapp-db/selectprofil.php");

            try
            {
                HttpResponseMessage response = await _client.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    Item = JsonSerializer.Deserialize<Profil>(content, _serializerOptions);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return Item;
        }

        /// <summary>
        /// Récupère la liste complète des profils (Historique)
        /// </summary>
        public async Task<List<Profil>> RecupTousLesProfils()
        {
            Items = new List<Profil>();
            Uri uri = new Uri("https://pepepc.ddns.net/coachapp-db/selecthistorique.php");

            try
            {
                HttpResponseMessage response = await _client.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    Items = JsonSerializer.Deserialize<List<Profil>>(content, _serializerOptions);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return Items;
        }
    }
}