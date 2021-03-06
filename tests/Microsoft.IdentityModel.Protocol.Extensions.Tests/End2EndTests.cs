//-----------------------------------------------------------------------
// Copyright (c) Microsoft Open Technologies, Inc.
// All Rights Reserved
// Apache License 2.0
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
// http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//-----------------------------------------------------------------------

using Microsoft.IdentityModel.Protocols;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.Threading;

namespace Microsoft.IdentityModel.Test
{
    /// <summary>
    /// 
    /// </summary>
    [TestClass]
    public class End2EndTests
    {
        public TestContext TestContext { get; set; }

        [ClassInitialize]
        public static void ClassSetup(TestContext testContext)
        {
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
        }

        [TestInitialize]
        public void Initialize()
        {
        }

        [TestMethod]
        [TestProperty("TestCaseID", "0226a914-61f6-41af-ba67-21261aeb356b")]
        [Description("Tests: OpenIdConnect")]
        [DeploymentItem("OpenIdConnectMetadata.json")]
        [DeploymentItem("JsonWebKeySet.json")]
        public void End2End_OpenIdConnect()
        {
            SigningCredentials rsaSigningCredentials = 
                new SigningCredentials(
                    KeyingMaterial.RsaSecurityKey_Private2048, 
                    SecurityAlgorithms.RsaSha1Signature, 
                    SecurityAlgorithms.Sha256Digest, 
                    new SecurityKeyIdentifier(new NamedKeySecurityKeyIdentifierClause("kid", "NGTFvdK-fythEuLwjpwAJOM9n-A"))
                    );

            //"<RSAKeyValue><Modulus>rCz8Sn3GGXmikH2MdTeGY1D711EORX/lVXpr+ecGgqfUWF8MPB07XkYuJ54DAuYT318+2XrzMjOtqkT94VkXmxv6dFGhG8YZ8vNMPd4tdj9c0lpvWQdqXtL1TlFRpD/P6UMEigfN0c9oWDg9U7Ilymgei0UXtf1gtcQbc5sSQU0S4vr9YJp2gLFIGK11Iqg4XSGdcI0QWLLkkC6cBukhVnd6BCYbLjTYy3fNs4DzNdemJlxGl8sLexFytBF6YApvSdus3nFXaMCtBGx16HzkK9ne3lobAwL2o79bP4imEGqg+ibvyNmbrwFGnQrBc1jTF9LyQX9q+louxVfHs6ZiVw==</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>"
            RSA rsa = KeyingMaterial.RsaSecurityKey_2048.GetAsymmetricAlgorithm(SecurityAlgorithms.RsaSha1Signature, false) as RSA;
            OpenIdConnectConfiguration configuration = OpenIdConnectConfigurationRetriever.GetAsync(OpenIdConfigData.OpenIdConnectMetadataFile, CancellationToken.None).Result;            
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            JwtSecurityToken jwt = tokenHandler.CreateToken(
                configuration.Issuer,
                IdentityUtilities.DefaultAudience,
                IdentityUtilities.DefaultClaimsIdentity,
                DateTime.UtcNow,
                DateTime.UtcNow + TimeSpan.FromHours(1),
                rsaSigningCredentials );

            TokenValidationParameters validationParameters =
                new TokenValidationParameters
                {
                    IssuerSigningTokens = configuration.SigningTokens,
                    ValidAudience = IdentityUtilities.DefaultAudience,
                    ValidIssuer = configuration.Issuer,
                };

            SecurityToken securityToken = null;
            tokenHandler.ValidateToken(jwt.RawData, validationParameters, out securityToken);
        }

        [TestMethod]
        [TestProperty("TestCaseID", "06fb4c0f-a67f-4924-aa06-0fc596fa3163")]
        [Description("Tests: WsFederation")]
        public void End2End_WsFederation()
        {

        }
    }
}