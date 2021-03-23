﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace RusDictionary.Modules
{
    public partial class CardIndexModule : UserControl
    {
        int ListBoxSelectedIndex;
        bool ListBoxPrev = true;
        string NameClickButtonInMenu;
        string TagButtonChange;
        /// <summary>
        /// Первый список элементов
        /// </summary>
        List<JSONArray> FirstListItems = new List<JSONArray>();
        /// <summary>
        /// Второй список элементов
        /// </summary>
        List<JSONArray> SecondListItems = new List<JSONArray>();
        /// <summary>
        /// Список элементов одной карточки
        /// </summary>
        List<JSONArray> CardItems = new List<JSONArray>();
        /// <summary>
        /// Список элементов всех карточек
        /// </summary>
        List<JSONArray> AllCardItems = new List<JSONArray>();
        /// <summary>
        /// Список элементов всех ящиков
        /// </summary>
        List<JSONArray> AllBoxItems = new List<JSONArray>();
        /// <summary>
        /// Список элементов всех букв
        /// </summary>
        List<JSONArray> AllLetterItems = new List<JSONArray>();
        /// <summary>
        /// Список элементов всех карточек-разделителей
        /// </summary>
        List<JSONArray> AllCardSeparatorItems = new List<JSONArray>();
        /// <summary>
        /// Список ящиков
        /// </summary>
        List<JSONArray> BoxItems = new List<JSONArray>();
        /// <summary>
        /// Список букв
        /// </summary>
        List<JSONArray> LetterItems = new List<JSONArray>();
        /// <summary>
        /// Список карт разделителей для одного ящика
        /// </summary>
        List<JSONArray> CardSeparatorItems = new List<JSONArray>();
        /// <summary>
        /// Список карт разделителей для одного ящика
        /// </summary>
        List<JSONArray> WordItems = new List<JSONArray>();
        /// <summary>
        /// Отслеживание нажатия на кнопку "Маркер"
        /// </summary>
        public static bool CardIndexMenuMarker = false;
        /// <summary>
        /// Отслеживание нажатия на кнопку "Карточка-разделитель"
        /// </summary>
        public static bool CardIndexMenuSeparator = false;
        /// <summary>
        /// Отслеживание нажатия на кнопку "Ящик"
        /// </summary>
        public static bool CardIndexMenuBox = false;
        /// <summary>
        /// Отслеживание нажатия на кнопку "Буква"
        /// </summary>
        public static bool CardIndexMenuLetter = false;
        /// <summary>
        /// Отслеживание нажатия на кнопку "Слово"
        /// </summary>
        public static bool CardIndexMenuWord = false;
        /// <summary>
        /// Маркер карточки
        /// </summary>
        string CardMarker;
        /// <summary>
        /// Текст с карточки
        /// </summary>
        string CardText;
        /// <summary>
        /// Примечание с карточки
        /// </summary>
        string CardNotes;
        /// <summary>
        /// Изображение карточки
        /// </summary>
        Image CardImage;
        /// <summary>
        /// Буква карточки
        /// </summary>
        string CardSymbol;
        /// <summary>
        /// Номер ящика карточки
        /// </summary>
        string CardNumberBox;
        /// <summary>
        /// ID ящика карточки
        /// </summary>
        string CardNumberBoxID;
        /// <summary>
        /// Разделитель ящика
        /// </summary>
        string CardSeparator;
        /// <summary>
        /// ID разделителя ящика
        /// </summary>
        string CardSeparatorID;
        /// <summary>
        /// Слово карточки
        /// </summary>
        string CardWord;
        /// <summary>
        /// Связанные со словом словосочения
        /// </summary>
        string CardRelatedCombinations;
        /// <summary>
        /// Значние слова
        /// </summary>
        string CardValue;
        /// <summary>
        /// Шифр Источника
        /// </summary>
        string CardSourceCode;
        /// <summary>
        /// Уточнение к источнику
        /// </summary>
        string CardSourceClarification;
        /// <summary>
        /// Пагинация карточки
        /// </summary>
        string CardPagination;
        /// <summary>
        /// Дата источника
        /// </summary>
        string CardSourceDate;
        /// <summary>
        /// Уточненная дата
        /// </summary>
        string CardSourceDateClarification;
        /// <summary>
        /// Использовался ли список второй раз
        /// </summary>
        bool UseSecondList = false;

        public CardIndexModule()
        {
            InitializeComponent();
            SetupSettingForElements();
            tlpWord.Width = panelWord.Width - 6;
            tlpWord.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right;
            tlpCard.Width = panelCard.Width - 6;
            tlpCard.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right;
        }
        /// <summary>
        /// Установка настроек элементов данного модуля
        /// </summary>
        void SetupSettingForElements()
        {
            foreach (ComboBox comboBox in MainForm.GetAll(this, typeof(ComboBox)))
            {
                comboBox.Font = new Font("Izhitsa", 16F, FontStyle.Regular, GraphicsUnit.Point, 0);                
            }
            foreach (TextBox textbox in MainForm.GetAll(this, typeof(TextBox)))
            {
                textbox.Font = new Font("Izhitsa", 16F, FontStyle.Regular, GraphicsUnit.Point, 0);
                textbox.ScrollBars = ScrollBars.Vertical;
            }
            foreach (ListBox listbox in MainForm.GetAll(this, typeof(ListBox)))
            {
                listbox.Font = new Font("Izhitsa", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
            }
            foreach (Label label in MainForm.GetAll(this, typeof(Label)))
            {
                if (label.Name == "laCardsFirstSeparator" || label.Name == "laCardsLastSeparator" || label.Name == "laCardsLetter" || label.Name == "laCardsNumberBox")
                {
                    label.Font = new Font("Izhitsa", 9.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
                }
                else
                {
                    label.Font = new Font("Izhitsa", 11F, FontStyle.Regular, GraphicsUnit.Point, 0);
                }
            }
            foreach (Button button in MainForm.GetAll(this, typeof(Button)))
            {
                if (button.Name == "buCardIndexCardsPrev" || button.Name == "buSelectWordPrev")
                {
                    button.Font = new Font("Izhitsa", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
                }
                else
                {
                    button.Font = new Font("Izhitsa", 16F, FontStyle.Regular, GraphicsUnit.Point, 204);
                }
            }
        }        
        void ActiveDown3button(bool parameter)
        {
            if (parameter == true)
            {
                foreach (Button button in MainForm.GetAll(this, typeof(Button)))
                {
                    if (button.Tag == "Insert" && MainForm.CanDoItList[0].CanInsert == 1.ToString() || button.Tag == "Update" && MainForm.CanDoItList[0].CanUpdate == 1.ToString() || button.Tag == "Delete" && MainForm.CanDoItList[0].CanDelete == 1.ToString() || button.Tag == "Select" && MainForm.CanDoItList[0].CanSelect == 1.ToString())
                    {
                        button.Enabled = parameter;
                    }                    
                }
            }
            else
            {
                buCardIndexListAdd.Enabled = parameter;
                buCardIndexListChange.Enabled = parameter;
                buCardIndexListDelete.Enabled = parameter;
            }
                          
        }
        private void buCardIndexInMenuButton_Click(object sender, EventArgs e)
        {
            EnableElement(false);
            Program.f1.PictAndLableWait(true);
            NameClickButtonInMenu = (sender as Button).Name.ToString();
            ClearMainList();
            Thread myThread = new Thread(new ParameterizedThreadStart(CreateFirstListItems)); //Создаем новый объект потока (функция, которая должна выпонится в фоновом режиме)
            myThread.Start(NameClickButtonInMenu); // Запускаем поток
            while (myThread.IsAlive)
            {
                Thread.Sleep(1);
                Application.DoEvents();
            }            
            switch (NameClickButtonInMenu)
            {
                case "buCardIndexMenuMarker":
                    {
                        for (int i = 0; i < FirstListItems.Count; i++)
                        {
                            lbCardIndexList.Items.Add((i + 1) + ") " + FirstListItems[i].Marker);
                        }
                        
                        ActiveDown3button(false);
                        break;
                    }
                case "buCardIndexMenuSeparator":
                    {
                        for (int i = 0; i < FirstListItems.Count; i++)
                        {
                            lbCardIndexList.Items.Add((i + 1) + ") " + FirstListItems[i].CardSeparator);
                        }
                        
                        ActiveDown3button(false);
                        break;
                    }
                case "buCardIndexMenuBox":
                    {
                        for (int i = 0; i < FirstListItems.Count; i++)
                        {
                            lbCardIndexList.Items.Add((i + 1) + ") " + FirstListItems[i].NumberBox);
                        }
                        
                        ActiveDown3button(false);
                        break;
                    }
                case "buCardIndexMenuLetter":
                    {
                        for (int i = 0; i < FirstListItems.Count; i++)
                        {
                            lbCardIndexList.Items.Add((i + 1) + ") " + FirstListItems[i].Symbol);
                        }
                        
                        ActiveDown3button(false);
                        break;
                    }
                case "buCardIndexMenuWord":
                    {
                        for (int i = 0; i < FirstListItems.Count; i++)
                        {
                            lbCardIndexList.Items.Add((i + 1) + ") " + FirstListItems[i].Word);
                        }
                        
                        ActiveDown3button(false);
                        break;
                    }
            }
            lbCardIndexList.Update();
            tcCards.SelectedTab = tpList;
            EnableElement(true);
            switch (NameClickButtonInMenu)
            {
                case "buCardIndexMenuMarker":
                    {
                        
                        ActiveDown3button(false);
                        break;
                    }
                case "buCardIndexMenuSeparator":
                    {
                        
                        ActiveDown3button(false);
                        break;
                    }
                case "buCardIndexMenuBox":
                    {
                        
                        ActiveDown3button(false);
                        break;
                    }
                case "buCardIndexMenuLetter":
                    {
                        
                        ActiveDown3button(false);
                        break;
                    }
                case "buCardIndexMenuWord":
                    {
                        
                        ActiveDown3button(false);
                        break;
                    }
            }
            Program.f1.PictAndLableWait(false);
        }
        void CreateSecondListItems(object NameButton)
        {
            if (SecondListItems != null)
            {
                SecondListItems.Clear();
            }
            
            /*
             Тут нужно поменять с индекса элементана на самэлемент
             */
            switch (NameClickButtonInMenu)
            {
                case "buCardIndexMenuSeparator":
                    {                       
                        string query = "SELECT * FROM flotation WHERE CardSeparator = " + ListBoxSelectedIndex;
                        JSON.Send(query, JSONFlags.Select);
                        SecondListItems = JSON.Decode();
                        break;
                    }
                case "buCardIndexMenuBox":
                    {                       
                        string query = "SELECT * FROM flotation WHERE NumberBox = " + ListBoxSelectedIndex;
                        JSON.Send(query, JSONFlags.Select);
                        SecondListItems = JSON.Decode();                       
                        break;
                    }
                case "buCardIndexMenuLetter":
                    {                        
                        string query = "SELECT * FROM flotation WHERE Symbol = " + ListBoxSelectedIndex;
                        JSON.Send(query, JSONFlags.Select);
                        SecondListItems = JSON.Decode();
                        break;
                    }
            }            
        }
        void CreateFirstListItems(object NameButton)
        {
            FirstListItems.Clear();
            switch (NameButton)
            {
                case "buCardIndexMenuMarker":
                    {
                        CardIndexMenuMarker = true;
                        CardIndexMenuSeparator = false;
                        CardIndexMenuBox = false;
                        CardIndexMenuLetter = false;
                        CardIndexMenuWord = false;
                        string query = "SELECT ID, Marker FROM cardindex";
                        JSON.Send(query, JSONFlags.Select);
                        FirstListItems = JSON.Decode();
                        break;
                    }
                case "buCardIndexMenuSeparator":
                    {
                        CardIndexMenuMarker = false;
                        CardIndexMenuSeparator = true;
                        CardIndexMenuBox = false;
                        CardIndexMenuLetter = false;
                        CardIndexMenuWord = false;
                        string query = "SELECT * FROM cardseparator";
                        JSON.Send(query, JSONFlags.Select);
                        FirstListItems = JSON.Decode();
                        break;
                    }
                case "buCardIndexMenuBox":
                    {
                        CardIndexMenuMarker = false;
                        CardIndexMenuSeparator = false;
                        CardIndexMenuBox = true;
                        CardIndexMenuLetter = false;
                        CardIndexMenuWord = false;
                        string query = "SELECT * FROM box";
                        JSON.Send(query, JSONFlags.Select);
                        FirstListItems = JSON.Decode();
                        break;
                    }
                case "buCardIndexMenuLetter":
                    {
                        CardIndexMenuMarker = false;
                        CardIndexMenuSeparator = false;
                        CardIndexMenuBox = false;
                        CardIndexMenuLetter = true;
                        CardIndexMenuWord = false;
                        string query = "SELECT * FROM letter";
                        JSON.Send(query, JSONFlags.Select);
                        FirstListItems = JSON.Decode();
                        break;
                    }
                case "buCardIndexMenuWord":
                    {
                        CardIndexMenuMarker = false;
                        CardIndexMenuSeparator = false;
                        CardIndexMenuBox = false;
                        CardIndexMenuLetter = false;
                        CardIndexMenuWord = true;
                        string query = "SELECT ID, Word, Value FROM flotation";
                        JSON.Send(query, JSONFlags.Select);
                        FirstListItems = JSON.Decode();
                        break;
                    }
            }
        }
        void ClearMainList()
        {            
            lbCardIndexList.ClearSelected();
            lbCardIndexList.Items.Clear();
        }
        private void buCardIndexMenuPrev_Click(object sender, EventArgs e)
        {
            Program.f1.TCPrev();
        }

        private void buCardIndexListPrev_Click(object sender, EventArgs e)
        {
            if (NameClickButtonInMenu == "buCardIndexMenuMarker" || NameClickButtonInMenu == "buCardIndexMenuWord")
            {
                tcCards.SelectedTab = tpCardsMenu;
            }
            else
            {                
                ActiveDown3button(false);
                UseSecondList = false;
                if (ListBoxPrev != true)
                {
                    CardIndexMenuWord = false;
                    ClearMainList();
                    ListBoxPrev = true;
                    switch (NameClickButtonInMenu)
                    {
                        case "buCardIndexMenuSeparator":
                            {
                                for (int i = 0; i < FirstListItems.Count; i++)
                                {
                                    lbCardIndexList.Items.Add((i + 1) + ") " + FirstListItems[i].CardSeparator);
                                }
                                break;
                            }
                        case "buCardIndexMenuBox":
                            {
                                for (int i = 0; i < FirstListItems.Count; i++)
                                {
                                    lbCardIndexList.Items.Add((i + 1) + ") " + FirstListItems[i].NumberBox);
                                }
                                break;
                            }
                        case "buCardIndexMenuLetter":
                            {
                                for (int i = 0; i < FirstListItems.Count; i++)
                                {
                                    lbCardIndexList.Items.Add((i + 1) + ") " + FirstListItems[i].Symbol);
                                }
                                break;
                            }
                    }
                }
                else
                {
                    tcCards.SelectedTab = tpCardsMenu;
                }                
            }            
        }
        void EnableElement(bool parameter)
        {
            foreach (Button button in MainForm.GetAll(tpList, typeof(Button)))
            {
                button.Enabled = parameter;
            }
            foreach (Button button in MainForm.GetAll(tpCardsMenu, typeof(Button)))
            {
                button.Enabled = parameter;
            }
            foreach (ListBox listbox in MainForm.GetAll(tpList, typeof(ListBox)))
            {
                listbox.Enabled = parameter;
            }
        }
        void EnableOnCardPage(bool parameter, TabPage tab)
        {            
            foreach (Button button in MainForm.GetAll(tab, typeof(Button)))
            {
                if (button.Name == "buCardIndexCardsPrev")
                {
                    button.Enabled = !parameter;
                }
                else if (button.Name == "buSelectWordPrev")
                {
                    button.Enabled = !parameter;
                }
                else
                {
                    button.Enabled = parameter;
                }
            }
            foreach (TextBox textBox in MainForm.GetAll(tab, typeof(TextBox)))
            {
                textBox.ReadOnly = !parameter;
            }
        }
        private void lbCardIndexList_DoubleClick(object sender, EventArgs e)
        {
            if (lbCardIndexList.SelectedItem != null)
            {
                EnableElement(false);
                ListBoxSelectedIndex = lbCardIndexList.SelectedIndex + 1;
                ListBoxPrev = false;
                if (CardIndexMenuMarker == true)
                {
                    Program.f1.PictAndLableWait(true);
                    Thread myThread = new Thread(new ParameterizedThreadStart(ShowCards)); //Создаем новый объект потока (функция, которая должна выпонится в фоновом режиме)
                    string[] SplitItem = lbCardIndexList.SelectedItem.ToString().Split(')', ' ');
                    myThread.Start(SplitItem.Last()); // Запускаем поток
                    while (myThread.IsAlive)
                    {
                        Thread.Sleep(1);
                        Application.DoEvents();
                    }
                    laCardsNumberCard.Text = "Текст карточки №" + CardMarker + ":";
                    //laCardsFirstSeparator.Text = "Разделитель: " + CardSeparator;
                    laCardsNumberBox.Text = "Ящик: " + CardNumberBox;
                    pbPictCard.BackgroundImage = CardImage;
                    tbCardText.Text = CardText;
                    tbCardSourceCode.Text = CardSourceCode;
                    tbCardSourceClarification.Text = CardSourceClarification;
                    tbCardPagination.Text = CardPagination;
                    tbCardSourceDate.Text = CardSourceDate;
                    tbCardSourceDateClarification.Text = CardSourceDateClarification;
                    tbCardNotes.Text = CardNotes;

                    EnableOnCardPage(false, tpCardsSelectCard);
                    tcCards.SelectedTab = tpCardsSelectCard;
                    Program.f1.PictAndLableWait(false);
                    EnableElement(true);
                }
                else if (CardIndexMenuWord == true)
                {
                    if (CardIndexMenuSeparator == false && CardIndexMenuBox == false && CardIndexMenuLetter == false)
                    {
                        Program.f1.PictAndLableWait(true);
                        string[] SplitItem = lbCardIndexList.SelectedItem.ToString().Split(new string[] { ") " }, StringSplitOptions.RemoveEmptyEntries);

                        int ID = lbCardIndexList.SelectedIndex;
                        Thread myThread = new Thread(() => ShowWordWithCard(FirstListItems[ID].ID)); //Создаем новый объект потока (функция, которая должна выпонится в фоновом режиме)
                        myThread.Start(); // Запускаем поток
                        while (myThread.IsAlive)
                        {
                            Thread.Sleep(1);
                            Application.DoEvents();
                        }
                        laCardsSelectWordNumberCard.Text = "Текст карточки №" + CardMarker + ":";
                        laCardsSelectWordSeparator.Text = "Разделитель: " + CardSeparator;
                        laCardsSelectWordNumberBox.Text = "Ящик: " + CardNumberBox;
                        laCardsSelectWordLetter.Text = "Буква: " + CardSymbol;                        
                        laCardsSelectWordWord.Text = "Слово: " + CardWord;
                        pbCardsSelectWordImage.BackgroundImage = CardImage;
                        tbCardsSelectWordText.Text = CardText;
                        tbCardsSelectWordValue.Text = CardValue;
                        tbCardsSelectWordSourceCode.Text = CardSourceCode;
                        tbCardsSelectWordSourceCodeClarification.Text = CardSourceClarification;
                        tbCardsSelectWordPagination.Text = CardPagination;
                        tbCardsSelectWordSourceDate.Text = CardSourceDate;
                        tbCardsSelectWordSourceDateClarification.Text = CardSourceDateClarification;
                        tbCardsSelectWordRelatedCombinations.Text = CardRelatedCombinations;
                        tbCardsSelectWordNotes.Text = CardNotes;
                        tcCards.SelectedTab = tpCardsSelectWord;

                        EnableOnCardPage(false, tpCardsSelectWord);
                        Program.f1.PictAndLableWait(false);
                        EnableElement(true);
                    }
                    else
                    {
                        Program.f1.PictAndLableWait(true);
                        string[] SplitItem = lbCardIndexList.SelectedItem.ToString().Split(new string[] { ") " }, StringSplitOptions.RemoveEmptyEntries);

                        int ID = lbCardIndexList.SelectedIndex;
                        Thread myThread = new Thread(() => ShowWordWithCard(SecondListItems[ID].ID)); //Создаем новый объект потока (функция, которая должна выпонится в фоновом режиме)
                        myThread.Start(); // Запускаем поток
                        while (myThread.IsAlive)
                        {
                            Thread.Sleep(1);
                            Application.DoEvents();
                        }
                        laCardsSelectWordNumberCard.Text = "Текст карточки №" + CardMarker + ":";
                        laCardsSelectWordSeparator.Text = "Разделитель: " + CardSeparator;
                        laCardsSelectWordNumberBox.Text = "Ящик: " + CardNumberBox;
                        laCardsSelectWordLetter.Text = "Буква: " + CardSymbol;
                        laCardsSelectWordWord.Text = "Слово: " + CardWord;
                        pbCardsSelectWordImage.BackgroundImage = CardImage;                        
                        tbCardsSelectWordText.Text = CardText;
                        tbCardsSelectWordValue.Text = CardValue;
                        tbCardsSelectWordSourceCode.Text = CardSourceCode;
                        tbCardsSelectWordSourceCodeClarification.Text = CardSourceClarification;
                        tbCardsSelectWordPagination.Text = CardPagination;
                        tbCardsSelectWordSourceDate.Text = CardSourceDate;
                        tbCardsSelectWordSourceDateClarification.Text = CardSourceDateClarification;
                        tbCardsSelectWordRelatedCombinations.Text = CardRelatedCombinations;
                        tbCardsSelectWordNotes.Text = CardNotes;
                        tcCards.SelectedTab = tpCardsSelectWord;

                        EnableOnCardPage(false, tpCardsSelectWord);
                        Program.f1.PictAndLableWait(false);
                        EnableElement(true);
                    }
                    //CardIndexMenuWord = false;
                    
                }
                else
                {
                    EnableElement(false);
                    Program.f1.PictAndLableWait(true);
                    Thread myThread = new Thread(new ParameterizedThreadStart(CreateSecondListItems)); //Создаем новый объект потока (функция, которая должна выпонится в фоновом режиме)
                    myThread.Start(NameClickButtonInMenu); // Запускаем поток
                    while (myThread.IsAlive)
                    {
                        Thread.Sleep(1);
                        Application.DoEvents();
                    }
                    ClearMainList();
                    EnableElement(true);
                    int CountList;
                    if (SecondListItems == null)
                    {
                        CountList = 0;
                    }
                    else
                    {
                        CountList = SecondListItems.Count;
                    }
                    
                    switch (NameClickButtonInMenu)
                    {
                        case "buCardIndexMenuSeparator":
                            {
                                for (int i = 0; i < CountList; i++)
                                {
                                    lbCardIndexList.Items.Add((i + 1) + ") " + SecondListItems[i].Word);
                                }
                                ActiveDown3button(false);
                                break;
                            }
                        case "buCardIndexMenuBox":
                            {
                                for (int i = 0; i < CountList; i++)
                                {
                                    lbCardIndexList.Items.Add((i + 1) + ") " + SecondListItems[i].Word);
                                }
                                ActiveDown3button(false);
                                break;
                            }
                        case "buCardIndexMenuLetter":
                            {
                                for (int i = 0; i < CountList; i++)
                                {
                                    lbCardIndexList.Items.Add((i + 1) + ") " + SecondListItems[i].Word);
                                }
                                ActiveDown3button(false);
                                break;
                            }
                    }
                    UseSecondList = true;
                    lbCardIndexList.Update();
                    CardIndexMenuWord = true;
                    lbCardIndexList.Visible = true;
                    Program.f1.PictAndLableWait(false);
                }
            }
        }
        /// <summary>
        /// Выбрать все ящики из БД
        /// </summary>
        void SelectAllBox()
        {
            string query = "SELECT * FROM Box";
            JSON.Send(query, JSONFlags.Select);
            AllBoxItems = JSON.Decode();
        }
        /// <summary>
        /// Выбрать все карточки разделители
        /// </summary>
        void SelectAllCardSeparator()
        {
            string query = "SELECT * FROM CardSeparator";
            JSON.Send(query, JSONFlags.Select);
            AllCardSeparatorItems = JSON.Decode();
        }        
        /// <summary>
        /// Выбрать все буквы
        /// </summary>        
        void SelectAllLetter()
        {
            string query = "SELECT * FROM Letter";
            JSON.Send(query, JSONFlags.Select);
            AllLetterItems = JSON.Decode();
        }
        void SelectAllCardNumber()
        {
            string query = "SELECT Marker FROM cardindex";
            JSON.Send(query, JSONFlags.Select);
            AllCardItems = JSON.Decode();
        }
        /// <summary>
        /// Выбирает нужный ящик по ID ящика
        /// </summary>
        /// <param name="BoxID"></param>
        void SelectBoxID(string BoxID)
        {
            string query = "SELECT * FROM Box WHERE ID = " + BoxID;
            JSON.Send(query, JSONFlags.Select);
            BoxItems = JSON.Decode();
        }
        /// <summary>
        /// Выбрать букву по её ID
        /// </summary>
        /// <param name="LetterID"></param>
        void SelectLetter(string LetterID)
        {
            string query = "SELECT * FROM Letter WHERE ID = " + LetterID;
            JSON.Send(query, JSONFlags.Select);
            LetterItems = JSON.Decode();
        }
        /// <summary>
        /// Выбрать слово по его ID
        /// </summary>
        /// <param name="WordID"></param>
        void SelectWord(string WordID)
        {
            string query = "SELECT * FROM flotation WHERE ID = " + WordID;
            JSON.Send(query, JSONFlags.Select);
            WordItems = JSON.Decode();
        }
        void ShowCards(object Number)
        {
            string query = "SELECT * FROM cardindex WHERE Marker = '" + Number + "'";
            JSON.Send(query, JSONFlags.Select);
            CardItems = JSON.Decode();
            CardMarker = CardItems[0].Marker;
            CardNumberBoxID = CardItems[0].NumberBox;
            query = "SELECT NumberBox FROM box WHERE ID = " + CardNumberBoxID;
            JSON.Send(query, JSONFlags.Select);
            CardItems[0].NumberBox = JSON.Decode()[0].NumberBox;
            CardNumberBox = CardItems[0].NumberBox;
            CardImage = DecodeImageFromDB(CardItems[0].Img);
            CardWord = CardItems[0].Word;
            CardText = CardItems[0].ImgText;
            CardRelatedCombinations = CardItems[0].RelatedCombinations;
            CardValue = CardItems[0].Value;
            CardSourceCode = CardItems[0].SourceCode;
            CardSourceClarification = CardItems[0].SourceClarification;
            CardPagination = CardItems[0].Pagination;
            CardSourceDate = CardItems[0].SourceDate;
            CardSourceDateClarification = CardItems[0].SourceDateClarification;
            CardNotes = CardItems[0].Notes;
        }
        void SelectCardSeparator(string ForCardSeparatorID)
        {
            string query = "SELECT * FROM CardSeparator WHERE ID = " + ForCardSeparatorID;
            JSON.Send(query, JSONFlags.Select);
            CardSeparatorItems = JSON.Decode();
        }
        void ShowWordWithCard(object ID)
        {              
            string query = "SELECT * FROM flotation WHERE ID = '" + ID + "'";
            JSON.Send(query, JSONFlags.Select);
            CardItems = JSON.Decode();
            List<JSONArray> AboutCard = new List<JSONArray>();
            query = "SELECT * FROM cardindex WHERE ID = " + CardItems[0].Card;
            JSON.Send(query, JSONFlags.Select);
            AboutCard = JSON.Decode();
            CardMarker = AboutCard[0].Marker;
            CardImage = DecodeImageFromDB(AboutCard[0].Img);
            CardText = AboutCard[0].ImgText;
            CardSourceCode = AboutCard[0].SourceCode;
            CardSourceClarification = AboutCard[0].SourceClarification;
            CardPagination = AboutCard[0].Pagination;
            CardSourceDate = AboutCard[0].SourceDate;
            CardSourceDateClarification = AboutCard[0].SourceDateClarification;
            CardNotes = AboutCard[0].Notes;
            //-------------\\
            query = "SELECT NumberBox FROM box WHERE ID = " + CardItems[0].NumberBox;
            JSON.Send(query, JSONFlags.Select);
            CardNumberBox = JSON.Decode()[0].NumberBox;
            //-------------\\
            query = "SELECT Symbol FROM letter WHERE ID = " + CardItems[0].Symbol;
            JSON.Send(query, JSONFlags.Select);
            CardSymbol = JSON.Decode()[0].Symbol;
            //-------------\\
            CardSeparatorID = CardItems[0].CardSeparator;
            query = "SELECT CardSeparator FROM cardseparator WHERE ID = " + CardSeparatorID;
            JSON.Send(query, JSONFlags.Select);
            CardSeparator = JSON.Decode()[0].CardSeparator;
            //-------------\\
            CardWord = CardItems[0].Word;
            CardValue = CardItems[0].Value;
            CardRelatedCombinations = CardItems[0].RelatedCombinations;
        }
        /// <summary>
        /// Кодирование изображения в base64
        /// </summary>
        /// <param name="FilePath">Путь к изображению</param>
        /// <returns>Закодированное изображение</returns>
        string CodeInBase64(string FilePath)
        {
            return Convert.ToBase64String(File.ReadAllBytes(FilePath));
        }
        /// <summary>
        /// Декодирование изображения
        /// </summary>
        /// <param name="Newbase64FromBD">Закодированное изображение</param>
        /// <returns>Декодированное изображение</returns>
        Image DecodeImageFromDB(string Newbase64FromBD)
        {
            if (Newbase64FromBD != "")
            {
                return Image.FromStream(new MemoryStream(Convert.FromBase64String(Newbase64FromBD)));
            }
            else
            {
                return Properties.Resources.noimage;
            }
        }

        private void buCardIndexCardsPrev_Click(object sender, EventArgs e)
        {
            tcCards.SelectedTab = tpList;
            EnableOnCardPage(true, tpCardsSelectCard);
        }
        private void buTest_Click(object sender, EventArgs e)
        {
            string query = "UPDATE `cardindex` SET `Notes` = 'Test' WHERE `Marker` = '5770005'";
            JSON.Send(query, JSONFlags.Update);
        }
        private void buCardIndexListDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Вы уверены, что хотите удалить данную запись?", "Удаление записи", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.No)
            {

            }
            if (result == DialogResult.Yes)
            {
                string[] NumberCardForDelete = (lbCardIndexList.SelectedItem.ToString()).Split(')', ' ');
                string query = "UPDATE `cardindex` SET `Marker` = is NULL,`CardSeparator` = is NULL,`NumberBox` = is NULL,`Symbol` = is NULL,`img` = is NULL, `imgText` = is NULL, `Notes` = is NULL, WHERE `Marker` = '" + NumberCardForDelete[2] + "'";                
                JSON.Send(query, JSONFlags.Update);
            }
        }
        private void lbCardIndexList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbCardIndexList.SelectedIndex != -1)
            {
                ActiveDown3button(true);
            }
        }
        private void buCardIndexListChange_Click(object sender, EventArgs e)
        {

            EnableElement(false);
            ListBoxSelectedIndex = lbCardIndexList.SelectedIndex + 1;
            Program.f1.PictAndLableWait(true);
            TagButtonChange = (sender as Button).Tag.ToString();            
            switch (NameClickButtonInMenu)
            {
                //Работает
                case "buCardIndexMenuMarker":
                    {
                        cbCardsInsertAndUpdateBox.Items.Clear();
                        List<Thread> MyThreads = new List<Thread>();
                        string[] SplitItem = lbCardIndexList.SelectedItem.ToString().Split(')', ' ');
                        MyThreads.Add(new Thread(() => ShowCards(SplitItem.Last()))); //Создаем новый объект потока (функция, которая должна выпонится в фоновом режиме)
                        MyThreads.Add(new Thread(() => SelectAllBox())); //Создаем новый объект потока (функция, которая должна выпонится в фоновом режиме)
                        MyThreads.Add(new Thread(() => SelectAllCardSeparator())); //Создаем новый объект потока (функция, которая должна выпонится в фоновом режиме)

                        MyThreads[0].Start();
                        while (MyThreads[0].IsAlive)
                        {
                            Thread.Sleep(1);
                            Application.DoEvents();
                        }
                        MyThreads[1].Start();
                        while (MyThreads[1].IsAlive)
                        {
                            Thread.Sleep(1);
                            Application.DoEvents();
                        }
                        MyThreads[2].Start();
                        while (MyThreads[2].IsAlive)
                        {
                            Thread.Sleep(1);
                            Application.DoEvents();
                        }
                        tbCardsInsertAndUpdateMarker.Text = CardMarker;

                        /* Изменить на выпадающий список*/
                        for (int i = 0; i < AllBoxItems.Count; i++)
                        {
                            cbCardsInsertAndUpdateBox.Items.Add(AllBoxItems[i].NumberBox);
                            if (CardNumberBox.Equals(AllBoxItems[i].NumberBox))
                            {
                                cbCardsInsertAndUpdateBox.SelectedIndex = i;
                            }
                        }                   
                        
                        pbCardsInsertAndUpdateImage.BackgroundImage = CardImage;
                        tbCardsInsertAndUpdateTextCard.Text = CardText;
                        tbCardsInsertAndUpdateSourceCode.Text = CardSourceCode;
                        tbCardsInsertAndUpdateSourceCodeClarification.Text = CardSourceClarification;
                        tbCardsInsertAndUpdatePagination.Text = CardPagination;
                        tbCardsInsertAndUpdateSourceDate.Text = CardSourceDate;
                        tbCardsInsertAndUpdateSourceDateClarification.Text = CardSourceDateClarification;
                        pbCardsInsertAndUpdateNotes.Text = CardNotes;
                        tcCards.SelectedTab = tpCardsInsertAndUpdateCard;
                        break;
                    }
                    //Криво
                case "buCardIndexMenuSeparator":
                    {
                        if (UseSecondList == true)
                        {
                            cbCardsInsertAndUpdateWordCard.Items.Clear();
                            cbCardsInsertAndUpdateWordBox.Items.Clear();
                            cbCardsInsertAndUpdateWordLetter.Items.Clear();
                            cbCardsInsertAndUpdateWordCardSeparator.Items.Clear();
                            List<Thread> MyThreads = new List<Thread>();
                            string[] SplitItem = lbCardIndexList.SelectedItem.ToString().Split(')', ' ');
                            string idWord = (SecondListItems[lbCardIndexList.SelectedIndex].ID).ToString();

                            MyThreads.Add(new Thread(() => ShowWordWithCard(idWord))); //Создаем новый объект потока (функция, которая должна выпонится в фоновом режиме)                                                                             
                            MyThreads.Add(new Thread(() => SelectAllCardNumber())); //Создаем новый объект потока (функция, которая должна выпонится в фоновом режиме)
                            MyThreads.Add(new Thread(() => SelectAllBox())); //Создаем новый объект потока (функция, которая должна выпонится в фоновом режиме)
                            MyThreads.Add(new Thread(() => SelectAllLetter())); //Создаем новый объект потока (функция, которая должна выпонится в фоновом режиме)
                            MyThreads.Add(new Thread(() => SelectAllCardSeparator())); //Создаем новый объект потока (функция, которая должна выпонится в фоновом режиме)

                            MyThreads[0].Start(); // Запускаем поток
                            while (MyThreads[0].IsAlive)
                            {
                                Thread.Sleep(1);
                                Application.DoEvents();
                            }
                            MyThreads[1].Start(); // Запускаем поток
                            while (MyThreads[1].IsAlive)
                            {
                                Thread.Sleep(1);
                                Application.DoEvents();
                            }
                            MyThreads[2].Start(); // Запускаем поток
                            while (MyThreads[2].IsAlive)
                            {
                                Thread.Sleep(1);
                                Application.DoEvents();
                            }
                            MyThreads[3].Start(); // Запускаем поток
                            while (MyThreads[3].IsAlive)
                            {
                                Thread.Sleep(1);
                                Application.DoEvents();
                            }
                            MyThreads[4].Start(); // Запускаем поток
                            while (MyThreads[4].IsAlive)
                            {
                                Thread.Sleep(1);
                                Application.DoEvents();
                            }

                            for (int i = 0; i < AllCardItems.Count; i++)
                            {
                                cbCardsInsertAndUpdateWordCard.Items.Add(AllCardItems[i].Marker);
                                if (AllCardItems[i].Marker == CardMarker)
                                {
                                    cbCardsInsertAndUpdateWordCard.SelectedIndex = i;
                                }
                            }
                            for (int i = 0; i < AllBoxItems.Count; i++)
                            {
                                cbCardsInsertAndUpdateWordBox.Items.Add(AllBoxItems[i].NumberBox);
                                if (AllBoxItems[i].NumberBox == CardNumberBox)
                                {
                                    cbCardsInsertAndUpdateWordBox.SelectedIndex = i;
                                }
                            }
                            for (int i = 0; i < AllLetterItems.Count; i++)
                            {
                                cbCardsInsertAndUpdateWordLetter.Items.Add(AllLetterItems[i].Symbol);
                                if (AllLetterItems[i].Symbol == CardSymbol)
                                {
                                    cbCardsInsertAndUpdateWordLetter.SelectedIndex = i;
                                }
                            }
                            for (int i = 0; i < AllCardSeparatorItems.Count; i++)
                            {
                                cbCardsInsertAndUpdateWordCardSeparator.Items.Add(AllCardSeparatorItems[i].CardSeparator);
                                if (AllCardSeparatorItems[i].CardSeparator == CardSeparator)
                                {
                                    cbCardsInsertAndUpdateWordCardSeparator.SelectedIndex = i;
                                }
                            }
                            pbCardsInsertAndUpdateWordImage.BackgroundImage = CardImage;
                            tbCardsInsertAndUpdateWordWord.Text = CardWord;
                            tbCardsInsertAndUpdateWordRelatedCombinations.Text = CardRelatedCombinations;
                            tbCardsInsertAndUpdateWordValue.Text = CardValue;
                            tcCards.SelectedTab = tpCardsInsertAndUpdateWord;
                        }
                        else
                        {
                            cbCardsInsertAndUpdateCardSeparatorBox.Items.Clear();
                            List<Thread> MyThreads = new List<Thread>();
                            string[] SplitItem = lbCardIndexList.SelectedItem.ToString().Split(')', ' ');
                            MyThreads.Add(new Thread(() => SelectAllBox())); //Создаем новый объект потока (функция, которая должна выпонится в фоновом режиме)

                            MyThreads.Add(new Thread(() => SelectAllCardSeparator())); //Создаем новый объект потока (функция, которая должна выпонится в фоновом режиме)
                            MyThreads[0].Start(); // Запускаем поток
                            while (MyThreads[0].IsAlive )
                            {
                                Thread.Sleep(1);
                                Application.DoEvents();
                            }
                            MyThreads[1].Start();
                            while (MyThreads[1].IsAlive)
                            {
                                Thread.Sleep(1);
                                Application.DoEvents();
                            }
                            int idSeparator = Convert.ToInt32(AllCardSeparatorItems[lbCardIndexList.SelectedIndex].ID);
                            for (int i = 0; i < AllBoxItems.Count; i++)
                            {
                                cbCardsInsertAndUpdateCardSeparatorBox.Items.Add(AllBoxItems[i].NumberBox);
                                if (AllCardSeparatorItems[lbCardIndexList.SelectedIndex].NumberBox == AllBoxItems[i].ID)
                                {
                                    cbCardsInsertAndUpdateCardSeparatorBox.SelectedIndex = i;
                                }
                            }
                            for (int i = 0; i < AllCardSeparatorItems.Count; i++)
                            {
                                if (AllCardSeparatorItems[i].ID.Equals(idSeparator.ToString()))
                                {
                                    tbCardsInsertAndUpdateCardSeparatorLetter.Text = AllCardSeparatorItems[i].CardSeparator;
                                    break;
                                }
                            }
                            tcCards.SelectedTab = tpCardsInsertAndUpdateCardSeparator;
                        }
                        break;
                    }
                    //Криво
                case "buCardIndexMenuBox":
                    {
                        if (UseSecondList == true)
                        {
                            cbCardsInsertAndUpdateWordCard.Items.Clear();
                            cbCardsInsertAndUpdateWordBox.Items.Clear();
                            cbCardsInsertAndUpdateWordLetter.Items.Clear();
                            cbCardsInsertAndUpdateWordCardSeparator.Items.Clear();
                            List<Thread> MyThreads = new List<Thread>();
                            string[] SplitItem = lbCardIndexList.SelectedItem.ToString().Split(')', ' ');
                            string idWord = (SecondListItems[lbCardIndexList.SelectedIndex].ID).ToString();

                            MyThreads.Add(new Thread(() => ShowWordWithCard(idWord))); //Создаем новый объект потока (функция, которая должна выпонится в фоновом режиме)                                                                             
                            MyThreads.Add(new Thread(() => SelectAllCardNumber())); //Создаем новый объект потока (функция, которая должна выпонится в фоновом режиме)
                            MyThreads.Add(new Thread(() => SelectAllBox())); //Создаем новый объект потока (функция, которая должна выпонится в фоновом режиме)
                            MyThreads.Add(new Thread(() => SelectAllLetter())); //Создаем новый объект потока (функция, которая должна выпонится в фоновом режиме)
                            MyThreads.Add(new Thread(() => SelectAllCardSeparator())); //Создаем новый объект потока (функция, которая должна выпонится в фоновом режиме)

                            MyThreads[0].Start(); // Запускаем поток
                            while (MyThreads[0].IsAlive)
                            {
                                Thread.Sleep(1);
                                Application.DoEvents();
                            }
                            MyThreads[1].Start(); // Запускаем поток
                            while (MyThreads[1].IsAlive)
                            {
                                Thread.Sleep(1);
                                Application.DoEvents();
                            }
                            MyThreads[2].Start(); // Запускаем поток
                            while (MyThreads[2].IsAlive)
                            {
                                Thread.Sleep(1);
                                Application.DoEvents();
                            }
                            MyThreads[3].Start(); // Запускаем поток
                            while (MyThreads[3].IsAlive)
                            {
                                Thread.Sleep(1);
                                Application.DoEvents();
                            }
                            MyThreads[4].Start(); // Запускаем поток
                            while (MyThreads[4].IsAlive)
                            {
                                Thread.Sleep(1);
                                Application.DoEvents();
                            }

                            for (int i = 0; i < AllCardItems.Count; i++)
                            {
                                cbCardsInsertAndUpdateWordCard.Items.Add(AllCardItems[i].Marker);
                                if (AllCardItems[i].Marker == CardMarker)
                                {
                                    cbCardsInsertAndUpdateWordCard.SelectedIndex = i;
                                }
                            }
                            for (int i = 0; i < AllBoxItems.Count; i++)
                            {
                                cbCardsInsertAndUpdateWordBox.Items.Add(AllBoxItems[i].NumberBox);
                                if (AllBoxItems[i].NumberBox == CardNumberBox)
                                {
                                    cbCardsInsertAndUpdateWordBox.SelectedIndex = i;
                                }
                            }
                            for (int i = 0; i < AllLetterItems.Count; i++)
                            {
                                cbCardsInsertAndUpdateWordLetter.Items.Add(AllLetterItems[i].Symbol);
                                if (AllLetterItems[i].Symbol == CardSymbol)
                                {
                                    cbCardsInsertAndUpdateWordLetter.SelectedIndex = i;
                                }
                            }
                            for (int i = 0; i < AllCardSeparatorItems.Count; i++)
                            {
                                cbCardsInsertAndUpdateWordCardSeparator.Items.Add(AllCardSeparatorItems[i].CardSeparator);
                                if (AllCardSeparatorItems[i].CardSeparator == CardSeparator)
                                {
                                    cbCardsInsertAndUpdateWordCardSeparator.SelectedIndex = i;
                                }
                            }
                            pbCardsInsertAndUpdateWordImage.BackgroundImage = CardImage;
                            tbCardsInsertAndUpdateWordWord.Text = CardWord;
                            tbCardsInsertAndUpdateWordRelatedCombinations.Text = CardRelatedCombinations;
                            tbCardsInsertAndUpdateWordValue.Text = CardValue;
                            tcCards.SelectedTab = tpCardsInsertAndUpdateWord;
                        }
                        else
                        {
                            List<Thread> MyThreads = new List<Thread>();
                            string[] SplitItem = lbCardIndexList.SelectedItem.ToString().Split(')', ' ');
                            string idbox = (FirstListItems[lbCardIndexList.SelectedIndex].ID).ToString();
                            MyThreads.Add(new Thread(() => SelectBoxID(idbox))); //Создаем новый объект потока (функция, которая должна выпонится в фоновом режиме)
                            MyThreads[0].Start(); // Запускаем поток
                            while (MyThreads[0].IsAlive)
                            {
                                Thread.Sleep(1);
                                Application.DoEvents();
                            }
                            tbCardsInsertAndUpdateBoxNumberBox.Text = BoxItems[0].NumberBox;
                            tcCards.SelectedTab = tpCardsInsertAndUpdateBox;
                        }
                        break;
                    }
                case "buCardIndexMenuLetter":
                    {
                        if (UseSecondList == true)
                        {
                            cbCardsInsertAndUpdateWordCard.Items.Clear();
                            cbCardsInsertAndUpdateWordBox.Items.Clear();
                            cbCardsInsertAndUpdateWordLetter.Items.Clear();
                            cbCardsInsertAndUpdateWordCardSeparator.Items.Clear();
                            List<Thread> MyThreads = new List<Thread>();
                            string[] SplitItem = lbCardIndexList.SelectedItem.ToString().Split(')', ' ');
                            string idWord = (SecondListItems[lbCardIndexList.SelectedIndex].ID).ToString();

                            MyThreads.Add(new Thread(() => ShowWordWithCard(idWord))); //Создаем новый объект потока (функция, которая должна выпонится в фоновом режиме)                                                                             
                            MyThreads.Add(new Thread(() => SelectAllCardNumber())); //Создаем новый объект потока (функция, которая должна выпонится в фоновом режиме)
                            MyThreads.Add(new Thread(() => SelectAllBox())); //Создаем новый объект потока (функция, которая должна выпонится в фоновом режиме)
                            MyThreads.Add(new Thread(() => SelectAllLetter())); //Создаем новый объект потока (функция, которая должна выпонится в фоновом режиме)
                            MyThreads.Add(new Thread(() => SelectAllCardSeparator())); //Создаем новый объект потока (функция, которая должна выпонится в фоновом режиме)

                            MyThreads[0].Start(); // Запускаем поток
                            while (MyThreads[0].IsAlive)
                            {
                                Thread.Sleep(1);
                                Application.DoEvents();
                            }
                            MyThreads[1].Start(); // Запускаем поток
                            while (MyThreads[1].IsAlive)
                            {
                                Thread.Sleep(1);
                                Application.DoEvents();
                            }
                            MyThreads[2].Start(); // Запускаем поток
                            while (MyThreads[2].IsAlive)
                            {
                                Thread.Sleep(1);
                                Application.DoEvents();
                            }
                            MyThreads[3].Start(); // Запускаем поток
                            while (MyThreads[3].IsAlive)
                            {
                                Thread.Sleep(1);
                                Application.DoEvents();
                            }
                            MyThreads[4].Start(); // Запускаем поток
                            while (MyThreads[4].IsAlive)
                            {
                                Thread.Sleep(1);
                                Application.DoEvents();
                            }

                            for (int i = 0; i < AllCardItems.Count; i++)
                            {
                                cbCardsInsertAndUpdateWordCard.Items.Add(AllCardItems[i].Marker);
                                if (AllCardItems[i].Marker == CardMarker)
                                {
                                    cbCardsInsertAndUpdateWordCard.SelectedIndex = i;
                                }
                            }
                            for (int i = 0; i < AllBoxItems.Count; i++)
                            {
                                cbCardsInsertAndUpdateWordBox.Items.Add(AllBoxItems[i].NumberBox);
                                if (AllBoxItems[i].NumberBox == CardNumberBox)
                                {
                                    cbCardsInsertAndUpdateWordBox.SelectedIndex = i;
                                }
                            }
                            for (int i = 0; i < AllLetterItems.Count; i++)
                            {
                                cbCardsInsertAndUpdateWordLetter.Items.Add(AllLetterItems[i].Symbol);
                                if (AllLetterItems[i].Symbol == CardSymbol)
                                {
                                    cbCardsInsertAndUpdateWordLetter.SelectedIndex = i;
                                }
                            }
                            for (int i = 0; i < AllCardSeparatorItems.Count; i++)
                            {
                                cbCardsInsertAndUpdateWordCardSeparator.Items.Add(AllCardSeparatorItems[i].CardSeparator);
                                if (AllCardSeparatorItems[i].CardSeparator == CardSeparator)
                                {
                                    cbCardsInsertAndUpdateWordCardSeparator.SelectedIndex = i;
                                }
                            }
                            pbCardsInsertAndUpdateWordImage.BackgroundImage = CardImage;
                            tbCardsInsertAndUpdateWordWord.Text = CardWord;
                            tbCardsInsertAndUpdateWordRelatedCombinations.Text = CardRelatedCombinations;
                            tbCardsInsertAndUpdateWordValue.Text = CardValue;
                            tcCards.SelectedTab = tpCardsInsertAndUpdateWord;
                        }
                        else
                        {
                            List<Thread> MyThreads = new List<Thread>();
                            string[] SplitItem = lbCardIndexList.SelectedItem.ToString().Split(')', ' ');
                            string idLetter = (FirstListItems[lbCardIndexList.SelectedIndex].ID).ToString();
                            MyThreads.Add(new Thread(() => SelectLetter(idLetter))); //Создаем новый объект потока (функция, которая должна выпонится в фоновом режиме)                        
                            MyThreads[0].Start(); // Запускаем поток
                            while (MyThreads[0].IsAlive)
                            {
                                Thread.Sleep(1);
                                Application.DoEvents();
                            }
                            tbCardsInsertAndUpdateLetter.Text = LetterItems[0].Symbol;
                            tcCards.SelectedTab = tpCardsInsertAndUpdateLetter;
                        }
                        
                        break;
                    }
                case "buCardIndexMenuWord":
                    {
                        cbCardsInsertAndUpdateWordCard.Items.Clear();
                        cbCardsInsertAndUpdateWordBox.Items.Clear();
                        cbCardsInsertAndUpdateWordLetter.Items.Clear();
                        cbCardsInsertAndUpdateWordCardSeparator.Items.Clear();
                        List<Thread> MyThreads = new List<Thread>();
                        string[] SplitItem = lbCardIndexList.SelectedItem.ToString().Split(')', ' ');
                        string idWord = (FirstListItems[lbCardIndexList.SelectedIndex].ID).ToString();

                        MyThreads.Add(new Thread(() => ShowWordWithCard(idWord))); //Создаем новый объект потока (функция, которая должна выпонится в фоновом режиме)                                                                             
                        MyThreads.Add(new Thread(() => SelectAllCardNumber())); //Создаем новый объект потока (функция, которая должна выпонится в фоновом режиме)
                        MyThreads.Add(new Thread(() => SelectAllBox())); //Создаем новый объект потока (функция, которая должна выпонится в фоновом режиме)
                        MyThreads.Add(new Thread(() => SelectAllLetter())); //Создаем новый объект потока (функция, которая должна выпонится в фоновом режиме)
                        MyThreads.Add(new Thread(() => SelectAllCardSeparator())); //Создаем новый объект потока (функция, которая должна выпонится в фоновом режиме)

                        MyThreads[0].Start(); // Запускаем поток
                        while (MyThreads[0].IsAlive)
                        {
                            Thread.Sleep(1);
                            Application.DoEvents();
                        }
                        MyThreads[1].Start(); // Запускаем поток
                        while (MyThreads[1].IsAlive)
                        {
                            Thread.Sleep(1);
                            Application.DoEvents();
                        }
                        MyThreads[2].Start(); // Запускаем поток
                        while (MyThreads[2].IsAlive)
                        {
                            Thread.Sleep(1);
                            Application.DoEvents();
                        }
                        MyThreads[3].Start(); // Запускаем поток
                        while (MyThreads[3].IsAlive)
                        {
                            Thread.Sleep(1);
                            Application.DoEvents();
                        }
                        MyThreads[4].Start(); // Запускаем поток
                        while (MyThreads[4].IsAlive)
                        {
                            Thread.Sleep(1);
                            Application.DoEvents();
                        }

                        for (int i = 0; i < AllCardItems.Count; i++)
                        {
                            cbCardsInsertAndUpdateWordCard.Items.Add(AllCardItems[i].Marker);
                            if (AllCardItems[i].Marker == CardMarker)
                            {
                                cbCardsInsertAndUpdateWordCard.SelectedIndex = i;
                            }
                        }
                        for (int i = 0; i < AllBoxItems.Count; i++)
                        {                            
                            cbCardsInsertAndUpdateWordBox.Items.Add(AllBoxItems[i].NumberBox);
                            if (AllBoxItems[i].NumberBox == CardNumberBox)
                            {
                                cbCardsInsertAndUpdateWordBox.SelectedIndex = i;
                            }
                        }
                        for (int i = 0; i < AllLetterItems.Count; i++)
                        {
                            cbCardsInsertAndUpdateWordLetter.Items.Add(AllLetterItems[i].Symbol);
                            if (AllLetterItems[i].Symbol == CardSymbol)
                            {
                                cbCardsInsertAndUpdateWordLetter.SelectedIndex = i;
                            }
                        }
                        for (int i = 0; i < AllCardSeparatorItems.Count; i++)
                        {
                            cbCardsInsertAndUpdateWordCardSeparator.Items.Add(AllCardSeparatorItems[i].CardSeparator);
                            if (AllCardSeparatorItems[i].CardSeparator == CardSeparator)
                            {
                                cbCardsInsertAndUpdateWordCardSeparator.SelectedIndex = i;
                            }
                        }
                        pbCardsInsertAndUpdateWordImage.BackgroundImage = CardImage;
                        tbCardsInsertAndUpdateWordWord.Text = CardWord;
                        tbCardsInsertAndUpdateWordRelatedCombinations.Text = CardRelatedCombinations;
                        tbCardsInsertAndUpdateWordValue.Text = CardValue;
                        tcCards.SelectedTab = tpCardsInsertAndUpdateWord;
                        break;
                    }
            }
            Program.f1.PictAndLableWait(false);
            EnableElement(true);            
        }

        private void buSelectWordPrev_Click(object sender, EventArgs e)
        {
            tcCards.SelectedTab = tpList;
            EnableOnCardPage(true, tpCardsSelectWord);
        }

        private void buttonInsertAndUpdatePrev_Click(object sender, EventArgs e)
        {
            switch (TagButtonChange)
            {
                case "Update":
                    {
                        DialogResult result = MessageBox.Show("Вы уверены, что хотите выйти? Все изменения не будут сохранены", "Возврат назад", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (result == DialogResult.Yes)
                        {
                            tcCards.SelectedTab = tpList;
                        }
                        break;
                    }
                case "Insert":
                    {
                        DialogResult result = MessageBox.Show("Вы уверены, что хотите выйти? Введенные не будут сохранены!", "Возврат назад", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (result == DialogResult.Yes)
                        {
                            tcCards.SelectedTab = tpList;
                        }
                        break;
                    }
            }
        }

        private void buttonInsertAndUpdateSave_Click(object sender, EventArgs e)
        {
            switch (tcCards.SelectedTab.Name)
            {
                case "tpCardsInsertAndUpdateCard":
                    {

                        break;
                    }
                case "tpCardsInsertAndUpdateWord":
                    {

                        break;
                    }
                case "tpCardsInsertAndUpdateLetter":
                    {

                        break;
                    }
                case "tpCardsInsertAndUpdateBox":
                    {

                        break;
                    }
                case "tpCardsInsertAndUpdateCardSeparator":
                    {

                        break;
                    }
            }                    
            MessageBox.Show("Операция выполнена успешно!", "Сохранение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void CardIndexModule_SizeChanged(object sender, EventArgs e)
        {
            /*foreach (ComboBox comboBox in MainForm.GetAll(tpCardsInsertAndUpdateCard, typeof(ComboBox)))
            {
                comboBox.Size = new Size(comboBox.Size.Width, tbCardsInsertAndUpdateMarker.Height);
                //SetComboBoxHeight(comboBox.Handle, tbCardsInsertAndUpdateMarker.Height);
                comboBox.Refresh();
            }
            foreach (ComboBox comboBox in MainForm.GetAll(tpCardsInsertAndUpdateWord, typeof(ComboBox)))
            {
                SetComboBoxHeight(comboBox.Handle, buCardsInsertAndUpdateWordWord.Height);
                comboBox.Refresh();
            }
            foreach (ComboBox comboBox in MainForm.GetAll(tpCardsInsertAndUpdateCardSeparator, typeof(ComboBox)))
            {
                SetComboBoxHeight(comboBox.Handle, tbCardsInsertAndUpdateCardSeparatorLetter.Height);
                comboBox.Refresh();
            }*/
        }
    }
}