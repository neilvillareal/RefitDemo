using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Refit;
using RefitDemo.Models;

namespace RefitDemo.API
{
    public interface ICoinsRepository
    {

        [Get("/api/v3/coins/list")]
        Task<List<Coin>> GetCoins();

        [Get("/api/v3/coins/{id}")]
        Task<Coin> GetCoin([AliasAs("id")] string coinName);
    }
}
