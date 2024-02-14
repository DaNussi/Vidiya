using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Vidiya.Managers
{
    public class AutoPlayManager
    {
        public static int AdFrequency = 10;
        public static int AutoQueueLength = 40;
        private static List<QueueItem> unusedAds = new List<QueueItem>();
        private static List<QueueItem> unusedMusic = new List<QueueItem>();

        public static Queue<QueueItem> queue = new Queue<QueueItem>();
        private static int queueCounter = 0;

        public static List<QueueItem> allMusic = new List<QueueItem>();
        public static List<QueueItem> allAds = new List<QueueItem>();

        public static List<Action> queueListeners = new List<Action>();

        public static void Init(Action<LogMessageType, string> log)
        {

            Scan();

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(50);
            timer.Tick += queue_Timer;
            timer.Start();
        }

        public static void Scan()
        {
            queue.Clear();
            unusedMusic.Clear();
            unusedAds.Clear();
            allMusic.Clear();
            allAds.Clear();

            foreach(var item in ContentManager.contentCollection)
            {
                List<Uri> content = ContentManager.get_content(item);
                List<QueueItem> queueItems = new List<QueueItem>();
                foreach(var uri in content)
                {
                    queueItems.Add(new QueueItem(uri, item.state));
                }

                switch (item.state.use) {
                    case ContentUse.ad:
                        allAds.AddRange(queueItems);
                        break;
                    case ContentUse.music:
                        allMusic.AddRange(queueItems);
                        break;
                }
            }

            triggerQueueChange();
        }

        public static void ExpandQueue()
        {
            if (unusedAds.Count <= 0) unusedAds.AddRange(allAds);
            if (unusedMusic.Count <= 0) unusedMusic.AddRange(allMusic);

            if (queueCounter % AdFrequency == 0 && unusedAds.Count > 0)
            {
                QueueItem ad = unusedAds.First();
                unusedAds.Remove(ad);
                queue.Enqueue(ad);
            } else if (unusedMusic.Count > 0)
            {
                QueueItem music = unusedMusic.First();
                unusedMusic.Remove(music);
                queue.Enqueue(music);
            }

            queueCounter++;
            triggerQueueChange();
        }

        private static void triggerQueueChange()
        {
            queueListeners.ForEach(a => a.Invoke());
        }

        private static void queue_Timer(object sender, EventArgs e)
        {
            if(queue.Count < AutoQueueLength) ExpandQueue();
        }

        public static void skip()
        {
            if (AutoPlayManager.queue.Count <= 0) return;
            QueueItem item = AutoPlayManager.queue.Dequeue();

            MediaPlayerManager.loadContent(item.uri);
            MediaPlayerManager.play();
            
        }
    }

    public class QueueItem
    {
        public Uri uri { get; }
        public ContentState originState { get; }
        
        public QueueItem(Uri uri, ContentState originState)
        {
            this.uri = uri;
            this.originState = originState;
        }
    
    }
}
