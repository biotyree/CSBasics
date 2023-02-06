using System;
using System.Collections.Generic;
using System.Text;

namespace hashes
{
    public class GhostsTask :
        IFactory<Document>, IFactory<Vector>, IFactory<Segment>, IFactory<Cat>, IFactory<Robot>,
        IMagic
    {
        private Cat catGhost;
        private Document documentGhost;
        private byte[] dataForDocument = new byte[] { 0 };
        private Robot robotGhost;
        private Segment segmentGhost;
        private Vector vectorGhost;

        public GhostsTask()
        {
            documentGhost = new Document("a", Encoding.UTF8, dataForDocument);
            vectorGhost = new Vector(0, 0);
            segmentGhost = new Segment(vectorGhost, vectorGhost);
            catGhost = new Cat("Barsik", "Siamese", new DateTime(2015, 7, 20));
            robotGhost = new Robot("42");
        }

        public void DoMagic()
        {
            catGhost.Rename("Kitty");
            dataForDocument[0] = 1;
            Robot.BatteryCapacity = (Robot.BatteryCapacity == 0 ? 1 : 0);
            vectorGhost.Add(new Vector(1, 1));
            vectorGhost.Add(new Vector(2, 2));
        }

        Document IFactory<Document>.Create() => documentGhost;
        Vector IFactory<Vector>.Create() => vectorGhost;
        Segment IFactory<Segment>.Create() => segmentGhost;
        public Cat Create() => catGhost;
        Robot IFactory<Robot>.Create() => robotGhost;
    }
}