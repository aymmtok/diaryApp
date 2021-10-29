using System;
using SQLite;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace diaryApp.DB
{
    public class DiaryDAO
    {
        readonly SQLiteAsyncConnection _database;

        // コンストラクタ（DB接続）
        public DiaryDAO(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Diary>().Wait();
        }

        // 非同期のデータ取得
        public Task<List<Diary>> GetDiaryAsync()
        {
            return _database.Table<Diary>().ToListAsync();
        }

        // 非同期のデータ取得（日付の降順でソート）
        public Task<List<Diary>> GetDiaryAsyncSorted()
        {
            return _database.Table<Diary>().OrderByDescending(x => x.Date).ToListAsync();
        }

        // IDを指定したデータ取得
        public Task<Diary> GetDiaryAsync(int id)
        {
            return _database.Table<Diary>().Where(i => i.DiaryID == id).FirstOrDefaultAsync();
        }

        // 指定した日付のデータを取得
        public Task<List<Diary>> GetDiaryAsyncDateTimeDAY(DateTime dateTime){
            DateTime nextDay = dateTime.AddDays(1);
            return _database.Table<Diary>().Where(i => (i.Date >= dateTime && i.Date < nextDay))
                .OrderByDescending(x => x.Date).ToListAsync();
        }

        // 指定した月のデータを取得
        public Task<List<Diary>> GetDiaryAsyncDateTimeMonth(DateTime dateTime)
        {
            DateTime nextMonth = dateTime.AddMonths(1);
            return _database.Table<Diary>().Where(i => (i.Date >= dateTime && i.Date < nextMonth))
                .OrderByDescending(x => x.Date).ToListAsync();
        }

        // 新規データの挿入
        public Task<int> SaveDiaryAsync(Diary diary)
        {
            if(diary.DiaryID != 0)
            {
                return _database.UpdateAsync(diary);
            }
            else
            {
                return _database.InsertAsync(diary);
            }
        }

        // 指定したデータの削除
        public Task<int> DeleteDiaryAsync(Diary diary)
        {
            return _database.DeleteAsync(diary);
        }

    }
}
