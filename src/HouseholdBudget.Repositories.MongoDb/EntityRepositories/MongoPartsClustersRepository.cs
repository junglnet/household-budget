using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BochkyOM.Common.Entities;
using BochkyOM.Common.Interfaces;
using BochkyOM.Repositories.DTO;
using BochkyOM.Repositories;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BochkyOM.Repositories.MongoDb.EntityRepositories
{
    public class MongoPartsClustersRepository : IRepository<Part>, 
        IRepository<ProductionSector>, 
        IRepository<PartCategory>, 
        IRepository<PartParametr>
    {

        private readonly MongoRepositoriesBundle _bundle;       
        private IRepository<Document> _documentsRepository;

        public MongoPartsClustersRepository(
            MongoRepositoriesBundle bundle,            
            IRepository<Document> documentsRepository)
        {
            _bundle = bundle;            
            _documentsRepository = documentsRepository;
        }


        // Add cluster

        async Task<string> IRepository<Part>.AddAsync(Part item)
        {
            item.Id = ObjectId.GenerateNewId().ToString();
            await AddOrUpdateCluster(item);
            await this._bundle.PartRepository.Collection.InsertOneAsync(item.ToDto());          
            return item.Id;
        }

        async Task<string> IRepository<ProductionSector>.AddAsync(ProductionSector item)
        {
            item.Id = ObjectId.GenerateNewId().ToString();
            await this._bundle.ProductionSectorRepository.Collection.InsertOneAsync(item.ToDto());
            return item.Id;
        }

        async Task<string> IRepository<PartCategory>.AddAsync(PartCategory item)
        {
            item.Id = ObjectId.GenerateNewId().ToString();
            await this._bundle.PartCategorytRepository.Collection.InsertOneAsync(item);
            return item.Id;
        }

        async Task<string> IRepository<PartParametr>.AddAsync(PartParametr item)
        {
            item.Id = ObjectId.GenerateNewId().ToString();
            await this._bundle.PartParametrRepository.Collection.InsertOneAsync(item.ToDto());
            return item.Id;
        }

        // Delete cluster

        Task<bool> IRepository<Part>.DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        Task<bool> IRepository<ProductionSector>.DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        Task<bool> IRepository<PartCategory>.DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        Task<bool> IRepository<PartParametr>.DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        // GetAll cluster

        Task<IEnumerable<Part>> IRepository<Part>.GetAllAsync()
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<ProductionSector>> IRepository<ProductionSector>.GetAllAsync()
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<PartCategory>> IRepository<PartCategory>.GetAllAsync()
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<PartParametr>> IRepository<PartParametr>.GetAllAsync()
        {
            throw new NotImplementedException();
        }

        // GetById cluster
        Task<Part> IRepository<Part>.GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        Task<ProductionSector> IRepository<ProductionSector>.GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        Task<PartCategory> IRepository<PartCategory>.GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        Task<PartParametr> IRepository<PartParametr>.GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        // GetByIds cluster

        Task<IEnumerable<Part>> IRepository<Part>.GetByIdsAsync(string[] ids)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<ProductionSector>> IRepository<ProductionSector>.GetByIdsAsync(string[] ids)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<PartCategory>> IRepository<PartCategory>.GetByIdsAsync(string[] ids)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<PartParametr>> IRepository<PartParametr>.GetByIdsAsync(string[] ids)
        {
            throw new NotImplementedException();
        }

        // Update cluster

        Task<bool> IRepository<Part>.UpdateAsync(Part item)
        {
            throw new NotImplementedException();
        }

        Task<bool> IRepository<ProductionSector>.UpdateAsync(ProductionSector item)
        {
            throw new NotImplementedException();
        }

        Task<bool> IRepository<PartCategory>.UpdateAsync(PartCategory item)
        {
            throw new NotImplementedException();
        }

        Task<bool> IRepository<PartParametr>.UpdateAsync(PartParametr item)
        {
            throw new NotImplementedException();
        }

        private async Task AddOrUpdateCluster(Part item)
        {
            if (item.PartParametrs != null)
            {
                foreach (PartParametr pp in item.PartParametrs)
                {
                    if (pp.Id == null)
                        pp.Id = await ((IRepository<PartParametr>)this).AddAsync(pp);
                    else
                        await ((IRepository<PartParametr>)this).UpdateAsync(pp);
                }
            }

            if (item.PartCategory.Id == null)
                item.PartCategory.Id = await ((IRepository<PartCategory>)this).AddAsync(item.PartCategory);
            else
                await ((IRepository<PartCategory>)this).UpdateAsync(item.PartCategory);

            if (item.ProductionSectors != null)
            {
                foreach (ProductionSector ps in item.ProductionSectors)
                {
                    if (ps.Id == null)
                        ps.Id = await ((IRepository<ProductionSector>)this).AddAsync(ps);
                    else
                        await ((IRepository<ProductionSector>)this).UpdateAsync(ps);
                }
            }


        }
    }
}
