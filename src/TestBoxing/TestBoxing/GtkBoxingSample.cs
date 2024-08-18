using System;
using Gtk;
using System.Collections;

namespace TestBoxing
{
class GtkBoxingSample : Window
{
Entry txtAmount = new Entry();
Entry txtBalance = new Entry("0");
Button btnDeposit = new Button("Deposit");
Button btnWithDraw = new Button("WithDraw");
TreeView grid = null;
ListStore _store = null;

public GtkBoxingSample () : base("Boxing Sample")
{
	DeleteEvent += new DeleteEventHandler(ClosedWindowEvent);
	btnDeposit.Clicked += new EventHandler(HandleDeposit);
	btnWithDraw.Clicked += new EventHandler(HandleWithDraw);
	this.BorderWidth = 6;
	var vbox = new VBox(false,8);
	var hbox = new HBox(false,6);
	hbox.Add(new Label("Amount: "));
	hbox.Add(txtAmount);
	HButtonBox hbb = new HButtonBox();
	hbb.Add(btnDeposit);
	hbb.Add(btnWithDraw);
	vbox.Add(hbox);
	vbox.Add(hbb);
	_store = new ListStore (typeof(string),typeof(string),typeof(string));
	ChangeBalance(DateTime.Now,'+',1000);
	grid = new TreeView(_store);
	string[] colnames = {"Date","Type","Amount"};
	for(int i = 0;i < colnames.Length;i++)
		grid.AppendColumn(new TreeViewColumn(colnames[i],new CellRendererText(),"text",i));
	HBox gridBox = new HBox();
	gridBox.Add(grid);
	HBox balanceBox = new HBox();
	balanceBox.Add(new Label("Balance"));
	txtBalance.IsEditable = false;
	balanceBox.Add(txtBalance);
	vbox.Add(gridBox);
	vbox.Add(balanceBox);
	this.Add(vbox);
	ShowAll();
}

void HandleDeposit (object sender,EventArgs e)
{
	double amount = Double.Parse (txtAmount.Text);
	ChangeBalance(DateTime.Now,'+',amount);
}

void HandleWithDraw (object sender,EventArgs e)
{
	double amount = Double.Parse(txtAmount.Text);
	ChangeBalance(DateTime.Now,'-',amount);
}



void ChangeBalance (DateTime date,char sign, double amount)
{
	double balance = 0.0;
	MovementItem item = new MovementItem (date, sign, amount);
	if(sign.Equals('+')) balance = Convert.ToDouble(txtBalance.Text) + amount; //unboxing
	if(sign.Equals('-')) balance = Convert.ToDouble(txtBalance.Text) - amount; //unboxing
	txtBalance.Text = Convert.ToString(balance); //boxing
			object objAmount = amount;
		_store.AppendValues (item.Date.ToString("dd/MM/yyyy"), //boxing date to string
				            Convert.ToString(item.Sign), //boxing char to string
			                objAmount.ToString()); //boxing double to string
}

void ClosedWindowEvent(object o, DeleteEventArgs args){
	Application.Quit();
}

public static void Main (string[] args)
{
	Application.Init();
	new GtkBoxingSample();
	Application.Run();
}
}
}
