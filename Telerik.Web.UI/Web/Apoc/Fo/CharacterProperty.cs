using System;

namespace Telerik.Web.Apoc.Fo
{
	// Token: 0x0200139E RID: 5022
	internal class CharacterProperty : Property
	{
		// Token: 0x0600D108 RID: 53512 RVA: 0x002E42D3 File Offset: 0x002E24D3
		public CharacterProperty(char character)
		{
			this.character = character;
		}

		// Token: 0x0600D109 RID: 53513 RVA: 0x002E42E2 File Offset: 0x002E24E2
		public override object GetObject()
		{
			return this.character;
		}

		// Token: 0x0600D10A RID: 53514 RVA: 0x002E42EF File Offset: 0x002E24EF
		public override char GetCharacter()
		{
			return this.character;
		}

		// Token: 0x0600D10B RID: 53515 RVA: 0x002E42F7 File Offset: 0x002E24F7
		public override string GetString()
		{
			return this.character.ToString();
		}

		// Token: 0x0400381C RID: 14364
		private char character;

		// Token: 0x0200139F RID: 5023
		internal class Maker : PropertyMaker
		{
			// Token: 0x0600D10C RID: 53516 RVA: 0x002E4304 File Offset: 0x002E2504
			public Maker(string propName) : base(propName)
			{
			}

			// Token: 0x0600D10D RID: 53517 RVA: 0x002E4310 File Offset: 0x002E2510
			public override Property Make(PropertyList propertyList, string value, FObj fo)
			{
				char character = value[0];
				return new CharacterProperty(character);
			}
		}
	}
}
