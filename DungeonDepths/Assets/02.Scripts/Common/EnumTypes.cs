namespace EnumTypes
{
    public enum MapType { KEY, NORMAL, BOSS, FINALBOSS, START }   //�� ���� : ��ư���� �̹����� �޶����
    public enum MapTheme { NONE, NATURE, DARK }                   //Stage �׸� : �̰ɷ� ��� ���������� ������ ����
    public enum MapDifficulty { NONE = 0, EASY, NORMAL, HARD }    //���̵� -> (int)������ �޾ƿͼ� ���� ���Ȱ��� �����ְų� �ؾ��ҵ� 
    public enum MonsterID { Chomper, SPITTER, BEHOLDER, MIMIC}

    public enum Window { MAINMENU, GAMEOVER, OPTION, MAP}
}
