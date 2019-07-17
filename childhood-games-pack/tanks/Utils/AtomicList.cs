using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace childhood_games_pack.tanks.Utils {
    public class AtomicList<T> : IEnumerable<T> {
        List<T> InternalCollection = new List<T>();

        public int Count() {
            return InternalCollection.Count;
        }

        public T this[int index] {
            get { return InternalCollection[index]; }
            set { InternalCollection[index] = value; }
        }

        public void Add(T value) {
            List<T> update = new List<T>(InternalCollection);
            update.Add(value);
            Interlocked.Exchange(ref InternalCollection, update);
        }

        public bool Remove(T value) {
            List<T> update = new List<T>(InternalCollection);
            bool removed = update.Remove(value);
            if (removed) {
                Interlocked.Exchange(ref InternalCollection, update);
            }

            return removed;
        }

        public IEnumerator<T> GetEnumerator() {
            return InternalCollection.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }
    }
}