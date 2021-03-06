﻿//-----------------------------------------------------------------------
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

using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.IdentityModel.Protocols
{
    /// <summary>
    /// Interface that defines a document retriever that returns the document as a string.
    /// </summary>
    public interface IDocumentRetriever
    {
        /// <summary>
        /// Obtains a document from an address.
        /// </summary>
        /// <param name="address">location of document.</param>
        /// <param name="cancel"><see cref="CancellationToken"/>.</param>
        /// <returns>document as a string.</returns>
        Task<string> GetDocumentAsync(string address, CancellationToken cancel);
    }
}
