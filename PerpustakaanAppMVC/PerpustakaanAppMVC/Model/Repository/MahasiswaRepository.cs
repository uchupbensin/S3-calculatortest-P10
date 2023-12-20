using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.OleDb;
using PerpustakaanAppMVC.Model.Entity;
using PerpustakaanAppMVC.Model.Context;

namespace PerpustakaanAppMVC.Model.Repository
{
    public class MahasiswaRepository
    {
        // Deklarasi objek connection
        private OleDbConnection _conn;

        // Constructor
        public MahasiswaRepository(DbContext context)
        {
            // Inisialisasi objek connection
            _conn = context.Conn;
        }
        public int Create(Mahasiswa mhs)
        {
            int result = 0;
            // deklarasi perintah SQL
            string sql = @"insert into mahasiswa (npm, nama, angkatan) values (@npm, @nama, @angkatan)";
            // membuat objek command menggunakan blok using
            using (OleDbCommand cmd = new OleDbCommand(sql, _conn))
            {
                // mendaftarkan parameter dan mengeset nilainya
                cmd.Parameters.AddWithValue("@npm", mhs.Npm);
                cmd.Parameters.AddWithValue("@nama", mhs.Nama);
                cmd.Parameters.AddWithValue("@angkatan", mhs.Angkatan);
                try
                {
                    // jalankan perintah INSERT dan tampung hasilnya ke dalam variabel result
                    result = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.Print("Create error: {0}", ex.Message);
                }
            }
            return result;
        }

       // public int Update(Mahasiswa mhs)
            // Tugas : Lengkapi kode untuk methode update

        // public int Delete(Mahasiswa mhs)
            // Tugas : Lengkapi kode untuk methode delete

        public List<Mahasiswa> ReadAll()
        {
            // membuat objek collection untuk menampung objek mahasiswa
            List<Mahasiswa> list = new List<Mahasiswa>();
            try
            {
                // deklarasi perintah SQL
                string sql = @"select npm, nama, angkatan from mahasiswa order by nama";
                // membuat objek command menggunakan blok using
                using (OleDbCommand cmd = new OleDbCommand(sql, _conn))
                {
                    // membuat objek dtr (data reader) untuk menampung result set (hasil perintah SELECT)
                using (OleDbDataReader dtr = cmd.ExecuteReader())
                    {
                        // panggil method Read untuk mendapatkan baris dari result set
                while (dtr.Read())
                        {
                            // proses konversi dari row result set ke object
                            Mahasiswa mhs = new Mahasiswa();
                            mhs.Npm = dtr["npm"].ToString();
                            mhs.Nama = dtr["nama"].ToString();
                            mhs.Angkatan = dtr["angkatan"].ToString();
                            // tambahkan objek mahasiswa ke dalam collection
                            list.Add(mhs);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print("ReadAll error: {0}", ex.Message);
            }
            return list;
        }

        public List<Mahasiswa> ReadByNama(string nama)
        {
            // membuat objek collection untuk menampung objek mahasiswa
            List<Mahasiswa> list = new List<Mahasiswa>();
            try
            {
                // deklarasi perintah SQL
                string sql = @"select npm, nama, angkatan from mahasiswa where nama like @namaorder by nama";
                // membuat objek command menggunakan blok using
                using (OleDbCommand cmd = new OleDbCommand(sql, _conn))
                {
                    // mendaftarkan parameter dan mengeset nilainya
                    cmd.Parameters.AddWithValue("@nama", "%" + nama + "%");
                    // membuat objek dtr (data reader) untuk menampung result set (hasil perintah SELECT)
                using (OleDbDataReader dtr = cmd.ExecuteReader())
                    {
                        // panggil method Read untuk mendapatkan baris dari result set
                while (dtr.Read())
                        {
                            // proses konversi dari row result set ke object
                            Mahasiswa mhs = new Mahasiswa();
                            mhs.Npm = dtr["npm"].ToString();
                            mhs.Nama = dtr["nama"].ToString();
                            mhs.Angkatan = dtr["angkatan"].ToString();
                            // tambahkan objek mahasiswa ke dalam collection
                            list.Add(mhs);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print("ReadByNama error: {0}",
               ex.Message);
            }
            return list;
        }

    }
}
