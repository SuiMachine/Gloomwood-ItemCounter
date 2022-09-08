using Gloomwood.Entity.Items;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ItemCounter
{
	public class ItemCounter : MonoBehaviour
	{
		private static ItemCounter instance;
		List<ItemPickup> treasure = new List<ItemPickup>();
		List<ItemPickup> currency = new List<ItemPickup>();


		public static void Initialize()
		{
			if(instance == null)
			{
				instance = new GameObject("Item Counter").AddComponent<ItemCounter>();
				DontDestroyOnLoad(instance.gameObject);
				Plugin.Msg("Initialized item counter object!");
			}
		}

		public static void Reinitialize()
		{
			if(instance != null)
			{
				instance.ReinistializeInstance();
			}
		}

		private void ReinistializeInstance()
		{
			StopAllCoroutines();
			StartCoroutine(Recount());
		}

		private IEnumerator Recount()
		{
			yield return null;
			treasure = FindObjectsOfType<ItemPickup>().Where(x => x.isActiveAndEnabled && x.EntityType == Gloomwood.Entity.EntityTypes.Pickups).Where(x => x.Config != null && x.Config.ItemType.ItemGroup == ItemGroups.Treasure).ToList();
			currency = FindObjectsOfType<ItemPickup>().Where(x => x.isActiveAndEnabled && x.EntityType == Gloomwood.Entity.EntityTypes.Pickups).Where(x => x.Config != null && x.Config.ItemType.ItemGroup == ItemGroups.Currency).ToList();

			yield return null;

			while (true)
			{
				for(int i = treasure.Count -1; i>=0 && i < treasure.Count; i--)
				{
					if(treasure[i] == null || !treasure[i].isActiveAndEnabled)
					{
						treasure.RemoveAt(i);
					}
					yield return null;
				}

				for (int i = currency.Count - 1; i >= 0 && i < currency.Count; i--)
				{
					if (currency[i] == null || !currency[i].isActiveAndEnabled)
					{
						currency.RemoveAt(i);
					}
					yield return null;
				}
				yield return null;
			}
		}

		void OnGUI()
		{
			GUILayout.BeginHorizontal();
			GUILayout.BeginVertical(GUI.skin.box);
			GUILayout.Label("Treasure left:" + treasure.Count.ToString());
			GUILayout.Label("Currency left:" + currency.Count.ToString());
			GUILayout.EndVertical();
			GUILayout.EndHorizontal();
		}
	}
}
