using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace cobaconnectdbonline
{
    public class SupabaseService
    {
        private readonly string _url = ConfigurationManager.AppSettings["SUPABASE_URL"];
        private readonly string _key = ConfigurationManager.AppSettings["SUPABASE_KEY"];

        private readonly HttpClient _client;

        public SupabaseService()
        {
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Add("apikey", _key);
            _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_key}");
        }

        // Mengambil semua data knowledge_sampah
        public async Task<List<dynamic>> GetAllDocuments()
        {
            // Mengambil id, isi, dan embedding dari tabel knowledge_sampah
            var response = await _client.GetAsync(
                $"{_url}/rest/v1/knowledge_sampah?select=id,isi,embedding");

            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<dynamic>>(json);
        }

        // Update embedding ke database
        public async Task UpdateEmbedding(string id, float[] embedding)
        {
            // Body request berisi vector embedding
            var body = new
            {
                embedding = embedding
            };

            var content = new StringContent(
                JsonConvert.SerializeObject(body),
                Encoding.UTF8,
                "application/json");

            // PATCH digunakan untuk update data berdasarkan id
            var request = new HttpRequestMessage(
            new HttpMethod("PATCH"),
            $"{_url}/rest/v1/knowledge_sampah?id=eq.{id}"
);

            request.Content = content;

            await _client.SendAsync(request);

        }

        // Vector Search (RAG Core)
        public async Task<(string isi, double similarity)> MatchDocument(float[] embedding)
        {
            // Memanggil RPC function "match_documents"
            // yang sudah dibuat di Supabase (PostgreSQL + pgvector)
            var body = new
            {
                query_embedding = embedding,
                match_count = 1
            };

            var content = new StringContent(
                JsonConvert.SerializeObject(body),
                Encoding.UTF8,
                "application/json");

            // Memanggil function RPC match_documents
            var response = await _client.PostAsync(
                $"{_url}/rest/v1/rpc/match_documents",
                content);

            var json = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<dynamic>>(json);

            // Jika ditemukan dokumen yang mirip
            if (result.Count > 0)
            {
                return (result[0].isi.ToString(), (double)result[0].similarity); // isi teks dokumen
            }

            return (null, 0);
        }

    }

}
