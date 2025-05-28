using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Maize.Models
{
    public class NftWithdrawTypedData
    {
        public Domain domain { get; set; }
        public Message message { get; set; }
        public string primaryType { get; set; }
        public Types types { get; set; }

        public class Domain
        {
            public BigInteger chainId { get; set; }
            public string name { get; set; }
            public string verifyingContract { get; set; }
            public string version { get; set; }
        }

        public class Message
        {
            public string owner { get; set; } //address
            public int accountID { get; set; } //uint32
            public int tokenID { get; set; } //uint16
            public string amount { get; set; } //unit96
            public int feeTokenID { get; set; } //uint16
            public string maxFee { get; set; } //unit96
            public string to { get; set; } //address
            public string extraData { get; set; } //bytes
            public BigInteger minGas { get; set; } //unit256
            public int validUntil { get; set; } //unit32
            public int storageID { get; set; } //unit32
        }

        public class Types
        {
            public List<Type> EIP712Domain { get; set; }
            public List<Type> Withdrawal { get; set; }
        }
    }
}
