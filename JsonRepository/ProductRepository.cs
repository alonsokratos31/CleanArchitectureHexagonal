﻿using Dominio.Entities;
using Dominio.Ports.Secundary;
using System;
using System.Text.Json;


namespace JsonRepository
{
    public class ProductRepository : IRepository
    {
        private readonly string _path;

        public ProductRepository(string path)
        {
            _path = path;
        }


        public List<Product> GetAll()
        {
            if (!File.Exists(_path))
            {
                return new List<Product>();
            }

            string jsonString = File.ReadAllText(_path);
            var products = JsonSerializer.Deserialize<List<Product>>(jsonString);

            return products ?? new List<Product>();
        }

        public void Save(Product product)
        {
            var products = GetAll();

            products.Add(product);

            var options = new JsonSerializerOptions {  WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(products, options);
            File.WriteAllText(_path, jsonString);

        }
    }
}
