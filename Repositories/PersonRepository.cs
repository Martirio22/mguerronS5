using mguerronS5.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace mguerronS5.Repositories
{
    public class PersonRepository
    {
        string _dbPath;
        private SQLiteConnection _conn;
        public string Message { get; set; }

        public void Init()
        {
            if (_conn is not null)
                return;
            _conn = new(_dbPath);
            _conn.CreateTable<Persona>();
        }

        public PersonRepository(string path)
        {
            _dbPath = path;
        }

        public void AddNewPerson(string name)
        {
            int result = 0;
            try
            {
                Init();
                if (string.IsNullOrEmpty(name))
                    throw new Exception("El nombre es requerido");
                Persona person = new() { Name = name };
                result = _conn.Insert(person);
                Message = string.Format("Dato ingresado", result, name);
            }
            catch (Exception ex)
            {
                Message = string.Format("Error " + ex.Message);
            }
        }

        public List<Persona> GetAllPerson()
        {
            try
            {
                Init();
                return _conn.Table<Persona>().ToList();
            }
            catch (Exception ex)
            {
                Message = string.Format("Error " + ex.Message);
            }
            return new List<Persona>();
        }

        public void UpdatePerson(int id, string name)
        {
            try
            {
                Init();

                if (string.IsNullOrWhiteSpace(name))
                    throw new Exception("El nombre es requerido");

                Persona person = _conn.Find<Persona>(id);

                if (person == null)
                    throw new Exception("La persona no existe");

                person.Name = name;

                int result = _conn.Update(person);

                Message = result > 0
                    ? "Persona actualizada correctamente"
                    : "No se pudo actualizar la persona";
            }
            catch (Exception ex)
            {
                Message = "Error: " + ex.Message;
            }
        }

        public void DeletePerson(int id)
        {
            try
            {
                Init();

                Persona person = _conn.Find<Persona>(id);

                if (person == null)
                    throw new Exception("La persona no existe");

                int result = _conn.Delete(person);

                Message = result > 0
                    ? "Persona eliminada correctamente"
                    : "No se pudo eliminar la persona";
            }
            catch (Exception ex)
            {
                Message = "Error: " + ex.Message;
            }
        }
    }
}
