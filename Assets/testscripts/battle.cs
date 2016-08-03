//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from: battle.proto
namespace battle
{
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"Battle")]
  public partial class Battle : global::ProtoBuf.IExtensible
  {
    public Battle() {}
    
    private long _id = default(long);
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"id", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(long))]
    public long id
    {
      get { return _id; }
      set { _id = value; }
    }
    private long _attacker = default(long);
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"attacker", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(long))]
    public long attacker
    {
      get { return _attacker; }
      set { _attacker = value; }
    }
    private long _defender = default(long);
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"defender", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(long))]
    public long defender
    {
      get { return _defender; }
      set { _defender = value; }
    }
    private int _type = default(int);
    [global::ProtoBuf.ProtoMember(4, IsRequired = false, Name=@"type", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int type
    {
      get { return _type; }
      set { _type = value; }
    }
    private long _timestamp = default(long);
    [global::ProtoBuf.ProtoMember(5, IsRequired = false, Name=@"timestamp", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(long))]
    public long timestamp
    {
      get { return _timestamp; }
      set { _timestamp = value; }
    }
    private int _star = default(int);
    [global::ProtoBuf.ProtoMember(6, IsRequired = false, Name=@"star", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int star
    {
      get { return _star; }
      set { _star = value; }
    }
    private int _destory = default(int);
    [global::ProtoBuf.ProtoMember(7, IsRequired = false, Name=@"destory", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int destory
    {
      get { return _destory; }
      set { _destory = value; }
    }
    private readonly global::System.Collections.Generic.List<Battle.ItemDefinition> _booty = new global::System.Collections.Generic.List<Battle.ItemDefinition>();
    [global::ProtoBuf.ProtoMember(8, Name=@"booty", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<Battle.ItemDefinition> booty
    {
      get { return _booty; }
    }
  
    private readonly global::System.Collections.Generic.List<Battle.ItemDefinition> _bonus = new global::System.Collections.Generic.List<Battle.ItemDefinition>();
    [global::ProtoBuf.ProtoMember(9, Name=@"bonus", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<Battle.ItemDefinition> bonus
    {
      get { return _bonus; }
    }
  
    private readonly global::System.Collections.Generic.List<Battle.UseArmyState> _useArmies = new global::System.Collections.Generic.List<Battle.UseArmyState>();
    [global::ProtoBuf.ProtoMember(10, Name=@"useArmies", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<Battle.UseArmyState> useArmies
    {
      get { return _useArmies; }
    }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"ItemDefinition")]
  public partial class ItemDefinition : global::ProtoBuf.IExtensible
  {
    public ItemDefinition() {}
    
    private int _itemID;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"itemID", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int itemID
    {
      get { return _itemID; }
      set { _itemID = value; }
    }
    private int _itemCount;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"itemCount", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int itemCount
    {
      get { return _itemCount; }
      set { _itemCount = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"UseArmyState")]
  public partial class UseArmyState : global::ProtoBuf.IExtensible
  {
    public UseArmyState() {}
    
    private int _sid = default(int);
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"sid", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int sid
    {
      get { return _sid; }
      set { _sid = value; }
    }
    private int _slevel = default(int);
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"slevel", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int slevel
    {
      get { return _slevel; }
      set { _slevel = value; }
    }
    private int _scount = default(int);
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"scount", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int scount
    {
      get { return _scount; }
      set { _scount = value; }
    }
    private int _slost = default(int);
    [global::ProtoBuf.ProtoMember(4, IsRequired = false, Name=@"slost", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int slost
    {
      get { return _slost; }
      set { _slost = value; }
    }
    private int _hid = default(int);
    [global::ProtoBuf.ProtoMember(5, IsRequired = false, Name=@"hid", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int hid
    {
      get { return _hid; }
      set { _hid = value; }
    }
    private int _hlevel = default(int);
    [global::ProtoBuf.ProtoMember(6, IsRequired = false, Name=@"hlevel", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int hlevel
    {
      get { return _hlevel; }
      set { _hlevel = value; }
    }
    private int _hstar = default(int);
    [global::ProtoBuf.ProtoMember(7, IsRequired = false, Name=@"hstar", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int hstar
    {
      get { return _hstar; }
      set { _hstar = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
}