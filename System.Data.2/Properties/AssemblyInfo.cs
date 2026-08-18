using System;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

[assembly: AssemblyVersion("4.0.0.0")]
[assembly: AssemblyDescription("System.Data.dll")]
[assembly: SecurityRules(SecurityRuleSet.Level1)]
[assembly: AssemblyProduct("Microsoft® .NET Framework")]
[assembly: AssemblyTitle("System.Data.dll")]
[assembly: AllowPartiallyTrustedCallers]
[assembly: CLSCompliant(true)]
[assembly: ComVisible(false)]
[assembly: AssemblyDefaultAlias("System.Data.dll")]
[assembly: AssemblySignatureKey("002400000c800000140100000602000000240000525341310008000001000100613399aff18ef1a2c2514a273a42d9042b72321f1757102df9ebada69923e2738406c21e5b801552ab8d200a65a235e001ac9adc25f2d811eb09496a4c6a59d4619589c69f5baf0c4179a47311d92555cd006acc8b5959f2bd6e10e360c34537a1d266da8085856583c85d81da7f3ec01ed9564c58d93d713cd0172c8e23a10f0239b80c96b07736f5d8b022542a4e74251a5f432824318b3539a5a087f8e53d2f135f9ca47f3bb2e10aff0af0849504fb7cea3ff192dc8de0edad64c68efde34c56d302ad55fd6e80f302d5efcdeae953658d3452561b5f36c542efdbdd9f888538d374cef106acf7d93a4445c3c73cd911f0571aaf3d54da12b11ddec375b3", "a5a866e1ee186f807668209f3b11236ace5e21f117803a3143abb126dd035d7d2f876b6938aaf2ee3414d5420d753621400db44a49c486ce134300a2106adb6bdb433590fef8ad5c43cba82290dc49530effd86523d9483c00f458af46890036b0e2c61d077d7fbac467a506eba29e467a87198b053c749aa2a4d2840c784e6d")]
[assembly: AssemblyCopyright("© Microsoft Corporation.  All rights reserved.")]
[assembly: InternalsVisibleTo("System.Data.DataSetExtensions, PublicKey=00000000000000000400000000000000")]
[assembly: InternalsVisibleTo("SqlAccess, PublicKey=0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
[assembly: DefaultDllImportSearchPaths(DllImportSearchPath.System32 | DllImportSearchPath.AssemblyDirectory)]
[assembly: ComCompatibleVersion(1, 0, 3300, 0)]
[assembly: AssemblyCompany("Microsoft Corporation")]
[assembly: AssemblyKeyFile("f:\\dd\\tools\\devdiv\\EcmaPublicKey.snk")]
[assembly: AssemblyDelaySign(true)]
[assembly: NeutralResourcesLanguage("en-US")]
[assembly: SatelliteContractVersion("4.0.0.0")]
[assembly: AssemblyInformationalVersion("4.8.9221.0")]
[assembly: AssemblyFileVersion("4.8.9221.0")]
[assembly: SecurityPermission(SecurityAction.RequestMinimum, SkipVerification = true)]
[module: CLSCompliant(true)]
[module: BidIdentity("System.Data.1")]
[module: BidMetaText(":FormatControl: InstanceID='' ")]
[module: BidMetaText("<CountHint> Trace=1200; Scope=250;")]
[module: BidMetaText("<Alias>  ds = System.Data;comm = System.Data.Common;odbc = System.Data.Odbc;oledb= System.Data.OleDb;prov = System.Data.ProviderBase;sc   = System.Data.Sql;sql  = System.Data.SqlClient;cqt  = System.Data.Common.CommandTrees;cqti = System.Data.Common.CommandTrees.Internal;esql = System.Data.Common.EntitySql;ec   = System.Data.EntityClient;dobj = System.Data.Objects;md   = System.Data.Metadata;ra   = System.Data.Query.ResultAssembly;pc   = System.Data.Query.PlanCompiler;iqt  = System.Data.Query.InternalTrees;mp   = System.Data.Mapping;upd  = System.Data.Mapping.Update;vgen = System.Data.Mapping.ViewGeneration;")]
[module: BidMetaText("<ApiGroup|ProviderBase|CPOOL> 0x00001000: Connection Pooling")]
[module: BidMetaText("<ApiGroup|SqlClient|DEP> 0x00002000: SqlDependency Notifications")]
[module: BidMetaText("<ApiGroup|System.Data.Query|RA> 0x00004000: Result Assembly")]
[module: BidMetaText("<ApiGroup|System.Data.Query.PlanCompiler|PC> 0x00008000: Plan Compilation")]
[module: BidMetaText("<ApiGroup|SqlClient|Correlation> 0x00040000: Correlation")]
