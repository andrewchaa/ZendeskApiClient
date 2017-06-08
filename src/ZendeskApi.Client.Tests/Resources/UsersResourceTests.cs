﻿using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging.Abstractions;
using Newtonsoft.Json;
using Xunit;
using ZendeskApi.Client.Resources;

namespace ZendeskApi.Client.Tests.Resources
{
    public class UsersResourceTests
    {
        private readonly IZendeskApiClient _client;
        private readonly UsersResource _resource;

        public UsersResourceTests()
        {
            _client = new DisposableZendeskApiClient((resource) => new UsersResourceSampleSite(resource));
            _resource = new UsersResource(_client, NullLogger.Instance);
        }

        /*
        Task<IEnumerable<User>> GetAllAsync();
        Task<IEnumerable<User>> GetAllUsersInGroupAsync(long groupId);
        Task<IEnumerable<User>> GetAllUsersInOrganizationAsync(long organizationId);
        Task<User> GetAsync(long userId);
        Task<IEnumerable<User>> GetAllAsync(long[] userIds);
        Task<IEnumerable<User>> GetAllByExternalIdsAsync(long[] externalIds);
        Task<User> GetRelatedUsersAsync(long userId);
        Task<User> PostAsync(UserRequest request);
        Task<User> PutAsync(UserRequest request);
        Task DeleteAsync(long userId);
         */

        [Fact]
        public async Task ShouldGetAllUsers()
        {
            var obj1 = new Contracts.Models.User
            {
                Id = 1245,
                Email = "Fu1@fu.com"
            };

            var obj2 = new Contracts.Models.User
            {
                Id = 1245,
                Email = "Fu2@fu.com"
            };

            var objs = (await _resource.GetAllAsync()).ToArray();

            Assert.Equal(2, objs.Length);
            Assert.Equal(JsonConvert.SerializeObject(obj1), JsonConvert.SerializeObject(objs[0]));
            Assert.Equal(JsonConvert.SerializeObject(obj2), JsonConvert.SerializeObject(objs[1]));
        }

        [Fact]
        public async Task ShouldGetAllUsersInGroup()
        {
            var obj1 = new Contracts.Models.User
            {
                Id = 523,
                Email = "Fu1@fu.com"
            };

            var obj2 = new Contracts.Models.User
            {
                Id = 552,
                Email = "Fu2@fu.com"
            };

            var objs = (await _resource.GetAllUsersInGroupAsync(456)).ToArray();

            Assert.Equal(2, objs.Length);
            Assert.Equal(JsonConvert.SerializeObject(obj1), JsonConvert.SerializeObject(objs[0]));
            Assert.Equal(JsonConvert.SerializeObject(obj2), JsonConvert.SerializeObject(objs[1]));
        }

        [Fact]
        public async Task ShouldGetAllUsersInOrganization()
        {
            var obj1 = new Contracts.Models.User
            {
                Id = 34634,
                Email = "Fu1@fu.com"
            };

            var obj2 = new Contracts.Models.User
            {
                Id = 2364,
                Email = "Fu2@fu.com"
            };

            var objs = (await _resource.GetAllUsersInOrganizationAsync(5002)).ToArray();

            Assert.Equal(2, objs.Length);
            Assert.Equal(JsonConvert.SerializeObject(obj1), JsonConvert.SerializeObject(objs[0]));
            Assert.Equal(JsonConvert.SerializeObject(obj2), JsonConvert.SerializeObject(objs[1]));
        }

        [Fact]
        public async Task ShouldGetTicket()
        {
            var response = await _resource.GetAsync(445L);

            Assert.NotNull(response.Id);
            Assert.Equal(445, response.Id);
            Assert.Equal("found@fu.com", response.Email);
        }
    }
}
