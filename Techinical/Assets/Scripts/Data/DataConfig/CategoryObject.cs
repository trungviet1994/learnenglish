using System;

[System.Serializable]
public class CategoryObject
{
    public int m_id;
    public int m_levelId;
    public string m_category;
    public string m_photo;
    public int m_score;
    public bool m_unlocked;

    public override string ToString()
    {
        return
            String.Format("{0}\t{1}\t{2}\t {3}\t{4}\t{5}", m_id, m_levelId, m_category, m_photo, m_score, m_unlocked);
    }
}
