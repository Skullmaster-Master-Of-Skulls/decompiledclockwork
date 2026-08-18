using System;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.Migrations.Model
{
	// Token: 0x02000710 RID: 1808
	public class MoveTableOperation : MigrationOperation
	{
		// Token: 0x06004946 RID: 18758 RVA: 0x0015F2DC File Offset: 0x0015D4DC
		[SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
		public MoveTableOperation(string name, string newSchema, object anonymousArguments = null) : base(anonymousArguments)
		{
			Check.NotEmpty(name, "name");
			this._name = name;
			this._newSchema = newSchema;
		}

		// Token: 0x17000AF9 RID: 2809
		// (get) Token: 0x06004947 RID: 18759 RVA: 0x0015F2FF File Offset: 0x0015D4FF
		public virtual string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17000AFA RID: 2810
		// (get) Token: 0x06004948 RID: 18760 RVA: 0x0015F307 File Offset: 0x0015D507
		public virtual string NewSchema
		{
			get
			{
				return this._newSchema;
			}
		}

		// Token: 0x17000AFB RID: 2811
		// (get) Token: 0x06004949 RID: 18761 RVA: 0x0015F310 File Offset: 0x0015D510
		public override MigrationOperation Inverse
		{
			get
			{
				DatabaseName databaseName = DatabaseName.Parse(this._name);
				return new MoveTableOperation(new DatabaseName(databaseName.Name, this.NewSchema).ToString(), databaseName.Schema, null)
				{
					IsSystem = this.IsSystem
				};
			}
		}

		// Token: 0x17000AFC RID: 2812
		// (get) Token: 0x0600494A RID: 18762 RVA: 0x0015F359 File Offset: 0x0015D559
		public override bool IsDestructiveChange
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000AFD RID: 2813
		// (get) Token: 0x0600494B RID: 18763 RVA: 0x0015F35C File Offset: 0x0015D55C
		// (set) Token: 0x0600494C RID: 18764 RVA: 0x0015F364 File Offset: 0x0015D564
		public string ContextKey { get; internal set; }

		// Token: 0x17000AFE RID: 2814
		// (get) Token: 0x0600494D RID: 18765 RVA: 0x0015F36D File Offset: 0x0015D56D
		// (set) Token: 0x0600494E RID: 18766 RVA: 0x0015F375 File Offset: 0x0015D575
		public bool IsSystem { get; internal set; }

		// Token: 0x17000AFF RID: 2815
		// (get) Token: 0x0600494F RID: 18767 RVA: 0x0015F37E File Offset: 0x0015D57E
		// (set) Token: 0x06004950 RID: 18768 RVA: 0x0015F386 File Offset: 0x0015D586
		public CreateTableOperation CreateTableOperation { get; internal set; }

		// Token: 0x04001B3B RID: 6971
		private readonly string _name;

		// Token: 0x04001B3C RID: 6972
		private readonly string _newSchema;
	}
}
