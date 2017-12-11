using System.Collections.Generic;
using Nethereum.Contracts;
using Nethereum.Geth;

namespace GenesisVision.TradeIpfsStorage
{
    internal class ContractHelper
    {
        #region ABI

        public static string ABI = @"[
    {
      ""constant"": false,
      ""inputs"": [
        {
          ""name"": ""managerId"",
          ""type"": ""string""
        },
        {
          ""name"": ""managerLogin"",
          ""type"": ""string""
        },
        {
          ""name"": ""brokerId"",
          ""type"": ""string""
        },
        {
          ""name"": ""managementFee"",
          ""type"": ""uint8""
        },
        {
          ""name"": ""successFee"",
          ""type"": ""uint8""
        }
      ],
      ""name"": ""registerManager"",
      ""outputs"": [
        {
          ""name"": """",
          ""type"": ""tuple"",
          ""components"": [
            {
              ""name"": ""id"",
              ""type"": ""string""
            },
            {
              ""name"": ""login"",
              ""type"": ""string""
            },
            {
              ""name"": ""level"",
              ""type"": ""uint8""
            },
            {
              ""name"": ""ipfsHash"",
              ""type"": ""string""
            }
          ]
        }
      ],
      ""payable"": false,
      ""stateMutability"": ""nonpayable"",
      ""type"": ""function""
    },
    {
      ""constant"": false,
      ""inputs"": [
        {
          ""name"": ""brokerId"",
          ""type"": ""string""
        },
        {
          ""name"": ""managerLogin"",
          ""type"": ""string""
        },
        {
          ""name"": ""brokerName"",
          ""type"": ""string""
        },
        {
          ""name"": ""managementFee"",
          ""type"": ""uint8""
        },
        {
          ""name"": ""successFee"",
          ""type"": ""uint8""
        }
      ],
      ""name"": ""getBrokerAddress"",
      ""outputs"": [
        {
          ""name"": """",
          ""type"": ""address""
        }
      ],
      ""payable"": false,
      ""stateMutability"": ""nonpayable"",
      ""type"": ""function""
    },
    {
      ""constant"": false,
      ""inputs"": [
        {
          ""name"": ""brokerId"",
          ""type"": ""string""
        },
        {
          ""name"": ""brokerContract"",
          ""type"": ""address""
        }
      ],
      ""name"": ""registerBroker"",
      ""outputs"": [
        {
          ""name"": """",
          ""type"": ""address""
        }
      ],
      ""payable"": false,
      ""stateMutability"": ""nonpayable"",
      ""type"": ""function""
    },
    {
      ""constant"": false,
      ""inputs"": [
        {
          ""name"": ""managerId"",
          ""type"": ""string""
        },
        {
          ""name"": ""ipfsHash"",
          ""type"": ""string""
        },
        {
          ""name"": ""managementFee"",
          ""type"": ""uint8""
        },
        {
          ""name"": ""successFee"",
          ""type"": ""uint8""
        }
      ],
      ""name"": ""updateManagerHistoryIpfsHash"",
      ""outputs"": [],
      ""payable"": false,
      ""stateMutability"": ""nonpayable"",
      ""type"": ""function""
    },
    {
      ""constant"": true,
      ""inputs"": [
        {
          ""name"": ""managerId"",
          ""type"": ""string""
        },
        {
          ""name"": ""ipfsHash"",
          ""type"": ""string""
        },
        {
          ""name"": ""managementFee"",
          ""type"": ""uint8""
        },
        {
          ""name"": ""successFee"",
          ""type"": ""uint8""
        }
      ],
      ""name"": ""getManagerHistoryIpfsHash"",
      ""outputs"": [
        {
          ""name"": """",
          ""type"": ""string""
        }
      ],
      ""payable"": false,
      ""stateMutability"": ""view"",
      ""type"": ""function""
    },
    {
      ""inputs"": [
        {
          ""name"": ""manager"",
          ""type"": ""address""
        }
      ],
      ""payable"": false,
      ""stateMutability"": ""nonpayable"",
      ""type"": ""function"",
      ""constant"": false,
      ""name"": ""setGenesisVisionManager"",
      ""outputs"": [
        {
          ""name"": """",
          ""type"": ""string""
        }
      ]
    },
    {
      ""payable"": false,
      ""stateMutability"": ""view"",
      ""type"": ""function"",
      ""inputs"": [
        {
          ""name"": ""managerId"",
          ""type"": ""string""
        }
      ],
      ""constant"": true,
      ""name"": ""getManagerLogin"",
      ""outputs"": [
        {
          ""name"": """",
          ""type"": ""string""
        }
      ]
    },
    {
      ""anonymous"": false,
      ""inputs"": [
        {
          ""indexed"": false,
          ""name"": ""brokerName"",
          ""type"": ""string""
        }
      ],
      ""name"": ""NewBroker"",
      ""type"": ""constructor"",
      ""payable"": false,
      ""stateMutability"": ""nonpayable""
    },
    {
      ""anonymous"": false,
      ""inputs"": [
        {
          ""indexed"": false,
          ""name"": ""brokerId"",
          ""type"": ""string""
        },
        {
          ""indexed"": false,
          ""name"": ""brokerName"",
          ""type"": ""string""
        }
      ],
      ""name"": ""NewBroker"",
      ""type"": ""event"",
      ""payable"": false,
      ""stateMutability"": ""nonpayable""
    },
    {
      ""anonymous"": false,
      ""inputs"": [
        {
          ""indexed"": false,
          ""name"": ""managerId"",
          ""type"": ""string""
        },
        {
          ""indexed"": false,
          ""name"": ""brokerId"",
          ""type"": ""string""
        }
      ],
      ""name"": ""NewManager"",
      ""type"": ""event""
    },
    {
      ""anonymous"": false,
      ""inputs"": [
        {
          ""indexed"": false,
          ""name"": ""managerId"",
          ""type"": ""string""
        },
        {
          ""indexed"": false,
          ""name"": ""brokerName"",
          ""type"": ""string""
        }
      ],
      ""name"": ""ManagerUpdated"",
      ""type"": ""event""
    },
    {
      ""anonymous"": false,
      ""inputs"": [
        {
          ""indexed"": false,
          ""name"": ""managerId"",
          ""type"": ""string""
        }
      ],
      ""name"": ""ManagerUpdated"",
      ""type"": ""event""
    }
  ]";

        #endregion

        private Function getManagerLogin;
        private Function getManagerHistoryIpfsHash;

        public ContractHelper(string gethHost, string contractAddress)
        {
            var web3 = new Web3Geth(gethHost);
            var contract = web3.Eth.GetContract(ABI, contractAddress);

            getManagerLogin = contract.GetFunction("getManagerLogin");
            getManagerHistoryIpfsHash = contract.GetFunction("getManagerHistoryIpfsHash");
        }

        public class ContractManager
        {
            public string id { get; set; }
            public string login { get; set; }
            public string ipfsHash { get; set; }
        }

        public object GetManagers(IEnumerable<string> managerIds)
        {
            return null;
        }
    }
}
