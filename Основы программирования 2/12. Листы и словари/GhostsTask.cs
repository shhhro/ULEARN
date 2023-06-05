using System;
using System.Data;
using System.Text;

namespace hashes;

public class GhostsTask : 
	IFactory<Document>, IFactory<Vector>, IFactory<Segment>, IFactory<Cat>, IFactory<Robot>, 
	IMagic
{
    private Vector ghostVector = new Vector(32, 45);
    private Segment ghostSegment = new Segment(new Vector(33, 54), new Vector(583, 33));
    private Document ghostDocument = new Document("be or not to be", Encoding.UTF32, new byte[] { 2, 4, 6 });
    private Cat ghostCat = new Cat("гав", "черный бродяжка", new DateTime(2033, 12, 23));
    private Robot ghostRobot = new Robot("c2p2", 23);
    private byte[] documentArray = new byte[] { 0, 4, 14, 5 };

    public void DoMagic()
	{
        ghostVector.Add(new Vector(333, 45));
        ghostSegment.Start.Add(ghostVector);
        ghostDocument = new Document("история о котенке гав", Encoding.UTF32, new byte[] { 2, 4, 6 });
        ghostCat.Rename("уже не гав");
        ghostRobot = new Robot("c2p2", ghostRobot.Battery - 10);
        documentArray = new byte[9];
	} 

	Vector IFactory<Vector>.Create()
	{
        return ghostVector;
	}

	Segment IFactory<Segment>.Create()
	{
        return ghostSegment;
	}

    Cat IFactory<Cat>.Create()
    {
        return ghostCat;
    }

    Document IFactory<Document>.Create()
    {
        return ghostDocument;
    }

    Robot IFactory<Robot>.Create()
    {
        ghostRobot.BatteryCapacity = 100;
        return ghostRobot;
    }
}