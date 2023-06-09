using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DominoClasses {
	public abstract class Train {
		private readonly List<Domino> dominoes;

		public int Count => dominoes.Count;

		public int EngineValue { get; set; }

		public bool IsEmpty => dominoes.Count == 0;

		public Domino LastDomino => dominoes.Last();

		public int PlayableValue => dominoes.Count == 1 ? EngineValue : LastDomino.Side2;

		public Domino this[int i] => dominoes[i];

		public void Add(Domino d) => dominoes.Add(d);

		protected bool IsPlayable(Domino d, out bool mustFlip) {;
			if (d.Side1 == LastDomino.Side1 || d.Side1 == LastDomino.Side2) {
				mustFlip = false;
				return true;
			}
			if (d.Side2 == LastDomino.Side2 || d.Side2 == LastDomino.Side2) {
				mustFlip = true;
				return true;
			}
			mustFlip = false;
			return false;
		}

		public abstract bool IsPlayable(Hand h, Domino d, out bool mustFlip);

		public void Play(Hand h, Domino d) {
			if (IsPlayable(h, d, out bool mustFlip)) {
				if (mustFlip) d.Flip();

				Add(d);
			}
			else throw new Exception($"Domino {d} does not match last domino in the train and cannot be played.");
		}

		public override string ToString() {
			string output = "";

			foreach (Domino p in dominoes) output += $"{p}\n";

			return output;
		}

		public Train() => dominoes = new List<Domino>();

		public Train(int eV) {
			dominoes = new List<Domino>();

			EngineValue = eV;
		}
	}
}