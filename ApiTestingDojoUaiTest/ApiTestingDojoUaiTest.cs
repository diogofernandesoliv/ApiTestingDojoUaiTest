using System;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using RestSharpTestingDojoUai;

namespace ApiTestingDojoUaiTest
{
    [TestFixture]
    public class ApiTestingDojoUaiTest
    {
        #region POST
        [Test]
        public void PostProduto()
        {
            #region DADOS ESPERADOS
            string urlEndPoint = "https://produtos-apirest.herokuapp.com/api/";
            string jsonProduto = @"{
                                      ""nome"": ""pizza"",
                                      ""quantidade"": 3,
                                      ""valor"": 20
                                 }";
            #endregion

            //Arrange
            var restClient = new RestClient(urlEndPoint);
            var restRequest = new RestRequest("produto", Method.POST) { RequestFormat = DataFormat.Json };

            restRequest.AddParameter("application/json", jsonProduto, ParameterType.RequestBody);

            //Act
            IRestResponse response = restClient.Execute(restRequest);
            var content = response.Content;

            //Assert
            Produto produtoEsperado = JsonConvert.DeserializeObject<Produto>(jsonProduto);
            Produto produtoAtual = JsonConvert.DeserializeObject<Produto>(content);

            Assert.AreEqual(produtoEsperado.nome, produtoAtual.nome);
            Assert.AreEqual(produtoEsperado.quantidade, produtoAtual.quantidade);
            Assert.AreEqual(produtoEsperado.valor, produtoAtual.valor);
        }
        #endregion

        #region GET
        [Test]
        public void GetProduto()
        {
            #region DADOS ESPERADOS
            string urlEndPoint = "https://produtos-apirest.herokuapp.com/api/";
            string jsonProduto = @"{
                                      ""id"": 821,
                                      ""nome"": ""carne"",
                                      ""quantidade"": 100,
                                      ""valor"": 50 
                                 }";
            #endregion

            //Arrage
            var restClient = new RestClient(urlEndPoint);
            var restRequest = new RestRequest("produto/" + 821, Method.GET) { RequestFormat = DataFormat.Json };

            //Act
            IRestResponse response = restClient.Execute(restRequest);
            var content = response.Content;

            //Assert
            Produto produtoEsperado = JsonConvert.DeserializeObject<Produto>(jsonProduto);
            Produto produtoAtual = JsonConvert.DeserializeObject<Produto>(content);

            Assert.AreEqual(produtoEsperado.id, produtoAtual.id);
            Assert.AreEqual(produtoEsperado.nome, produtoAtual.nome);
            Assert.AreEqual(produtoEsperado.quantidade, produtoAtual.quantidade);
            Assert.AreEqual(produtoEsperado.valor, produtoAtual.valor);
        }
        #endregion

        #region PUT
        [Test]
        public void PutProduto()
        {
            #region DADOS ESPERADOS
            string urlEndPoint = "https://produtos-apirest.herokuapp.com/api/";
            string jsonProduto = @"{
                                      ""id"": 830,
                                      ""nome"": ""hamburguer"",
                                      ""quantidade"": 100,
                                      ""valor"": 9 
                                 }";
            #endregion
            
            //Arrange
            var restClient = new RestClient(urlEndPoint);
            var restRequest = new RestRequest("produto", Method.PUT) { RequestFormat = DataFormat.Json };

            restRequest.AddParameter("application/json", jsonProduto, ParameterType.RequestBody);

            //Act
            IRestResponse response = restClient.Execute(restRequest);
            var content = response.Content;

            //Assert
            Produto produtoEsperado = JsonConvert.DeserializeObject<Produto>(jsonProduto);
            Produto produtoAtual = JsonConvert.DeserializeObject<Produto>(content);

            Assert.AreEqual(produtoEsperado.id, produtoAtual.id);
            Assert.AreEqual(produtoEsperado.nome, produtoAtual.nome);
            Assert.AreEqual(produtoEsperado.quantidade, produtoAtual.quantidade);
            Assert.AreEqual(produtoEsperado.valor, produtoAtual.valor);
        }
        #endregion

        #region DELETE
        [Test]
        public void DeleteProduto()
        {
            #region DADOS ESPERADOS
            string urlEndPoint = "https://produtos-apirest.herokuapp.com/api/";
            string jsonProduto = @"{
                                      ""id"": 825,
                                      ""nome"": ""pizza"",
                                      ""quantidade"": 3,
                                      ""valor"": 20 
                                 }";
            #endregion

            //Arrange
            var restClient = new RestClient(urlEndPoint);
            var restRequest = new RestRequest("produto", Method.DELETE) { RequestFormat = DataFormat.Json };

            restRequest.AddParameter("application/json", jsonProduto, ParameterType.RequestBody);

            //Act
            IRestResponse response = restClient.Execute(restRequest);
            var content = response.Content;

            //DELETA DEVE SER VAZIO
            //Assert
            Produto produtoEsperado = null;
            Produto produtoAtual = JsonConvert.DeserializeObject<Produto>(content);

            Assert.AreEqual(produtoEsperado, produtoAtual);
        }
        #endregion

        #region GET OUTRA API
        [Test]
        public void GetCivilization()
        {
            #region DADOS ESPERADOS
            string name = "Aztecs";
            string expansion = "The Conquerors";
            string army_type = "Infantry and Monk";
            List<string> unique_unit = new List<string> { "https://age-of-empires-2-api.herokuapp.com/api/v1/unit/jaguar_warrior" };
            List<string> unique_tech = new List<string> { "https://age-of-empires-2-api.herokuapp.com/api/v1/technology/garland_wars" };
            string team_bonus = "Relics generate +33% gold";
            List<string> civilization_bonus = new List<string> { "Villagers carry +5"
                                                                 ,"Military units created 15% faster"
                                                                 ,"+5 Monk hit points for each Monastery technology"
                                                                 ,"Loom free" };
            #endregion

            //Arrange
            RestApiHelper<Civilization> restApi = new RestApiHelper<Civilization>();
            var restUrl = restApi.SetUrl("api/v1/civilization/1");
            var restRequest = restApi.CreateGetRequest();

            //Act
            var response = restApi.GetResponse(restUrl, restRequest);
            Civilization content = restApi.GetContent<Civilization>(response);

            //Assert
            Assert.AreEqual(content.name, name);
            Assert.AreEqual(content.expansion, expansion);
            Assert.AreEqual(content.army_type, army_type);
            Assert.AreEqual(content.unique_unit, unique_unit);
            Assert.AreEqual(content.unique_tech, unique_tech);
            Assert.AreEqual(content.team_bonus, team_bonus);
            Assert.AreEqual(content.civilization_bonus, civilization_bonus);
        }

        [Test]
        public void GetCivilizationWrong()
        {
            #region DADOS ESPERADOS
            string name = "Vikings";
            string expansion = "Age of Kings";
            string army_type = "Infantry and naval";
            List<string> unique_unit = new List<string> { "https://age-of-empires-2-api.herokuapp.com/api/v1/unit/berserk"
                                                         ,"https://age-of-empires-2-api.herokuapp.com/api/v1/unit/longboat" };
            List<string> unique_tech = new List<string> { "https://age-of-empires-2-api.herokuapp.com/api/v1/technology/berserkergang" };
            string team_bonus = "Docks are 25% cheaper";
            List<string> civilization_bonus = new List<string> { "Warships cost 20% less"
                                                                 ,"Infantry have +10% HP in Feudal Age/ +15% in Castle Age/ +20% in Imperial Age"
                                                                 ,"Wheelbarrow and Hand Cart are free"
                                                                 ,"Erro" };
            #endregion

            //Arrange
            RestApiHelper<Civilization> restApi = new RestApiHelper<Civilization>();
            var restUrl = restApi.SetUrl("api/v1/civilization/18");
            var restRequest = restApi.CreateGetRequest();

            //Act
            var response = restApi.GetResponse(restUrl, restRequest);
            Civilization content = restApi.GetContent<Civilization>(response);

            //Assert
            Assert.AreEqual(content.name, name);
            Assert.AreEqual(content.expansion, expansion);
            Assert.AreEqual(content.army_type, army_type);
            Assert.AreEqual(content.unique_unit, unique_unit);
            Assert.AreEqual(content.unique_tech, unique_tech);
            Assert.AreEqual(content.team_bonus, team_bonus);
            Assert.AreEqual(content.civilization_bonus, civilization_bonus);
        }
        #endregion
    }
}
