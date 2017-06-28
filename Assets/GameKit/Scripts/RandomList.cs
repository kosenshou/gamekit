using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RandomList : List<int>{

	private int amountToRandom, totalAmount;

	public RandomList (int amountToRandom, int totalAmount) {
		this.amountToRandom = amountToRandom;
		this.totalAmount = totalAmount;

		GenerateList ();
	}

	void GenerateList () {
		while (Count < amountToRandom) {
			int y = Random.Range (0, totalAmount);
			bool sameValue = false;

			if (Count == 0)
				Insert (Count, y);

			for (int x = 0; x < Count; x++) {
				if (this[x] == y) {
					sameValue = true;
					break;
				}
			}

			if (!sameValue && Count != 0)
				Insert(Count, y);
		}
	}

	public void Debug(string title) {
		foreach (int i in this) {
            UnityEngine.Debug.Log (title + ": " + i);
		}
	}
}
