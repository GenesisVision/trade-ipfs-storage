using System;
using System.Threading.Tasks;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;
using Nethereum.Web3;

namespace GenesisVision.PlatformContract
{
    public class PlatformContractService
    {
        private readonly Web3 web3;

        public static string ABI = @"[
    {
      'constant': false,
      'inputs': [
        {
          'name': 'admin',
          'type': 'address'
        }
      ],
      'name': 'setGenesisVisionAdmin',
      'outputs': [],
      'payable': false,
      'stateMutability': 'nonpayable',
      'type': 'function'
    },
    {
      'constant': true,
      'inputs': [
        {
          'name': 'managerId',
          'type': 'string'
        }
      ],
      'name': 'getManager',
      'outputs': [
        {
          'name': '',
          'type': 'string'
        },
        {
          'name': '',
          'type': 'string'
        },
        {
          'name': '',
          'type': 'string'
        },
        {
          'name': '',
          'type': 'string'
        }
      ],
      'payable': false,
      'stateMutability': 'view',
      'type': 'function'
    },
    {
      'constant': false,
      'inputs': [
        {
          'name': 'managerId',
          'type': 'string'
        },
        {
          'name': 'managerLogin',
          'type': 'string'
        },
        {
          'name': 'brokerId',
          'type': 'string'
        },
        {
          'name': 'managementFee',
          'type': 'uint8'
        },
        {
          'name': 'successFee',
          'type': 'uint8'
        }
      ],
      'name': 'registerManager',
      'outputs': [],
      'payable': false,
      'stateMutability': 'nonpayable',
      'type': 'function'
    },
    {
      'constant': true,
      'inputs': [
        {
          'name': 'brokerId',
          'type': 'string'
        }
      ],
      'name': 'getBroker',
      'outputs': [
        {
          'name': '',
          'type': 'address'
        },
        {
          'name': '',
          'type': 'string'
        },
        {
          'name': '',
          'type': 'string'
        },
        {
          'name': '',
          'type': 'string'
        }
      ],
      'payable': false,
      'stateMutability': 'view',
      'type': 'function'
    },
    {
      'constant': false,
      'inputs': [
        {
          'name': 'brokerId',
          'type': 'string'
        },
        {
          'name': 'brokerContract',
          'type': 'address'
        },
        {
          'name': 'name',
          'type': 'string'
        },
        {
          'name': 'host',
          'type': 'string'
        }
      ],
      'name': 'registerBroker',
      'outputs': [],
      'payable': false,
      'stateMutability': 'nonpayable',
      'type': 'function'
    },
    {
      'constant': false,
      'inputs': [
        {
          'name': 'managerId',
          'type': 'string'
        },
        {
          'name': 'ipfsHash',
          'type': 'string'
        }
      ],
      'name': 'updateManagerHistoryIpfsHash',
      'outputs': [],
      'payable': false,
      'stateMutability': 'nonpayable',
      'type': 'function'
    },
    {
      'constant': true,
      'inputs': [
        {
          'name': 'managerId',
          'type': 'string'
        }
      ],
      'name': 'getManagerHistoryIpfsHash',
      'outputs': [
        {
          'name': '',
          'type': 'string'
        }
      ],
      'payable': false,
      'stateMutability': 'view',
      'type': 'function'
    },
    {
      'constant': true,
      'inputs': [
        {
          'name': 'managerId',
          'type': 'string'
        }
      ],
      'name': 'getManagerLogin',
      'outputs': [
        {
          'name': '',
          'type': 'string'
        }
      ],
      'payable': false,
      'stateMutability': 'view',
      'type': 'function'
    },
    {
      'inputs': [],
      'payable': false,
      'stateMutability': 'nonpayable',
      'type': 'constructor'
    },
    {
      'anonymous': false,
      'inputs': [
        {
          'indexed': false,
          'name': 'brokerId',
          'type': 'string'
        },
        {
          'indexed': false,
          'name': 'brokerContract',
          'type': 'address'
        }
      ],
      'name': 'NewBroker',
      'type': 'event'
    },
    {
      'anonymous': false,
      'inputs': [
        {
          'indexed': false,
          'name': 'managerId',
          'type': 'string'
        },
        {
          'indexed': false,
          'name': 'brokerId',
          'type': 'string'
        }
      ],
      'name': 'NewManager',
      'type': 'event'
    },
    {
      'anonymous': false,
      'inputs': [
        {
          'indexed': false,
          'name': 'managerId',
          'type': 'string'
        }
      ],
      'name': 'ManagerUpdated',
      'type': 'event'
    }
  ]";

        public static string BYTE_CODE = "";

        public static Task<string> DeployContractAsync(Web3 web3, string addressFrom, HexBigInteger gas = null, HexBigInteger valueAmount = null)
        {
            return web3.Eth.DeployContract.SendRequestAsync(ABI, BYTE_CODE, addressFrom, gas, valueAmount);
        }

        private Contract contract;

        public PlatformContractService(Web3 web3, string address)
        {
            this.web3 = web3;
            this.contract = web3.Eth.GetContract(ABI, address);
        }

        public Function GetFunctionSetGenesisVisionAdmin()
        {
            return contract.GetFunction("setGenesisVisionAdmin");
        }

        public Function GetFunctionGetManager()
        {
            return contract.GetFunction("getManager");
        }

        public Function GetFunctionRegisterManager()
        {
            return contract.GetFunction("registerManager");
        }

        public Function GetFunctionGetBroker()
        {
            return contract.GetFunction("getBroker");
        }

        public Function GetFunctionRegisterBroker()
        {
            return contract.GetFunction("registerBroker");
        }

        public Function GetFunctionUpdateManagerHistoryIpfsHash()
        {
            return contract.GetFunction("updateManagerHistoryIpfsHash");
        }

        public Function GetFunctionGetManagerHistoryIpfsHash()
        {
            return contract.GetFunction("getManagerHistoryIpfsHash");
        }

        public Function GetFunctionGetManagerLogin()
        {
            return contract.GetFunction("getManagerLogin");
        }

        public Event GetEventNewBroker()
        {
            return contract.GetEvent("NewBroker");
        }

        public Event GetEventNewManager()
        {
            return contract.GetEvent("NewManager");
        }

        public Event GetEventManagerUpdated()
        {
            return contract.GetEvent("ManagerUpdated");
        }

        public Task<string> GetManagerHistoryIpfsHashAsyncCall(string managerId)
        {
            var function = GetFunctionGetManagerHistoryIpfsHash();
            return function.CallAsync<string>(managerId);
        }

        public Task<string> GetManagerLoginAsyncCall(string managerId)
        {
            var function = GetFunctionGetManagerLogin();
            return function.CallAsync<string>(managerId);
        }

        public Task<GetManagerDTO> GetManagerAsyncCall(string managerId)
        {
            var function = GetFunctionGetManager();
            return function.CallAsync<GetManagerDTO>(managerId);
        }

        public Task<GetBrokerDTO> GetBrokerAsyncCall(string brokerId)
        {
            var function = GetFunctionGetBroker();
            return function.CallAsync<GetBrokerDTO>(brokerId);
        }

        public Task<string> SetGenesisVisionAdminAsync(string addressFrom, string admin, HexBigInteger gas = null, HexBigInteger valueAmount = null)
        {
            var function = GetFunctionSetGenesisVisionAdmin();
            return function.SendTransactionAsync(addressFrom, gas, valueAmount, admin);
        }

        public Task<string> RegisterManagerAsync(string addressFrom, string managerId, string managerLogin, string brokerId, byte managementFee, byte successFee, HexBigInteger gas = null, HexBigInteger valueAmount = null)
        {
            var function = GetFunctionRegisterManager();
            return function.SendTransactionAsync(addressFrom, gas, valueAmount, managerId, managerLogin, brokerId, managementFee, successFee);
        }

        public Task<string> RegisterBrokerAsync(string addressFrom, string brokerId, string brokerContract, string name, string host, HexBigInteger gas = null, HexBigInteger valueAmount = null)
        {
            var function = GetFunctionRegisterBroker();
            return function.SendTransactionAsync(addressFrom, gas, valueAmount, brokerId, brokerContract, name, host);
        }

        public Task<string> UpdateManagerHistoryIpfsHashAsync(string addressFrom, string managerId, string ipfsHash, HexBigInteger gas = null, HexBigInteger valueAmount = null)
        {
            var function = GetFunctionUpdateManagerHistoryIpfsHash();
            return function.SendTransactionAsync(addressFrom, gas, new HexBigInteger(BigInteger.Parse("26000000000")), valueAmount, managerId, ipfsHash);
        }
    }


    [FunctionOutput]
    public class GetManagerDTO
    {

        [Parameter("string", "", 1)]
        public string B { get; set; }

        [Parameter("string", "", 2)]
        public string C { get; set; }

        [Parameter("string", "", 3)]
        public string D { get; set; }

        [Parameter("string", "", 4)]
        public string E { get; set; }
    }

    [FunctionOutput]
    public class GetBrokerDTO
    {

        [Parameter("address", "", 1)]
        public string B { get; set; }

        [Parameter("string", "", 2)]
        public string C { get; set; }

        [Parameter("string", "", 3)]
        public string D { get; set; }

        [Parameter("string", "", 4)]
        public string E { get; set; }
    }



    public class NewBrokerEventDTO
    {

        [Parameter("string", "brokerId", 1, false)]
        public string BrokerId { get; set; }

        [Parameter("address", "brokerContract", 2, false)]
        public string BrokerContract { get; set; }
    }

    public class NewManagerEventDTO
    {

        [Parameter("string", "managerId", 1, false)]
        public string ManagerId { get; set; }

        [Parameter("string", "brokerId", 2, false)]
        public string BrokerId { get; set; }
    }

}
