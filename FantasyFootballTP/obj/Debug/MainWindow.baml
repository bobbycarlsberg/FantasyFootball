   M S B A M L   `   `   `  ����  FantasyFootballTP"    FantasyFootballTP.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -   5          �Name$  DatagridWindow��5   7    � Title$ 
MainWindow��6	   $
��1000��6   $
��1000��6*   .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   6,   �� 5      $��* �6   6)   �� 5	      $��* �6   6)   �� 5
      $��* �6   6)    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   6.   �� 5      $
��Auto �6   6.   �� 5      $
��Auto �6   6.   .+�5   
   ��  �� Orientation$ 
Horizontal=�6   .+�5      �� -   5         ��Name$ Goalies��5      $	��GKS��6&   6N   �� 5      -   5        $ 	Defenders��5      $
��DEFS��6(   6S   �� 5      -   5        $
 mids��5      $
��MIDS��6#   6I   �� 5      -   5        $
 fwds��5      $
��FWDS��6#   6I   �� 5      -   5        $ teams��5      $��Teams��6$   6L   5      �� 5   
   $ 
Horizontal=�6   $��1��6.   .+�5      �� $	��100��6S   ��6   �� *CurrentGameWeek+ �� ElementName$ DatagridWindow��6a   �� 5      $	��100��6N   ��6   �� *
FormLength+$ DatagridWindow��6\   �� 5      $	��100��6R   ��6   �� *FutureFixtures+$ DatagridWindow��6`   �� 5      $	��100��6L   ��6   �� *
MaxPrice+$ DatagridWindow��6Z   �� 5      -   5        $ Price��5      $��Get Players at price��6$   6[   �� 5       -   5        $ 
TeamOfWeek��5       $��Team of week��6)   6]   �� 5!      ��6   �� *
Squad.Cost+ �� StringFormat$ N2��$ DatagridWindow��6c   5"      &   System.Windows.Controls.DataGrid  5$   
   $��1��6      AutoGenerateColumns .  ��6    �� *	TeamRanks+$ DatagridWindow��	   Columns	 5%      0  *System.Windows.Controls.DataGridTextColumn  5&      ,  &System.Windows.Controls.DataGridColumn
   Header$

 Team��6%   1  +System.Windows.Controls.DataGridBoundColumn   Binding 63   �� *Team.LongName+6W     5'      $
 Matches��6%    66   �� *Team.MatchCount+6\     5(      $
 Home GK��6%    66   �� *AvghomeGKPPoints+6]     5)      $
 Home Def��6%    67   �� *AvgHomeDefPoints+6^     5*      $
 Home Mid��6%    67   �� *AvgHomeMidPoints+6^     5+      $
 Home Att��6%    67   �� *AvgHomeFWDPoints+6^     5,      $
 Away GK��6%    66   �� *AvgAwayGKPPoints+6]     5-      $
 Away Def��6%    67   �� *AvgAwayDefPoints+6^     5.      $
 Away Mid��6%    67   �� *AvgAwayMidPoints+6^     5/      $
 Away Att��6%    67   �� *AvgAwayFWDPoints+6^   51        53   
   $��1��6   $��1��6    $��2��60   "   VerticalScrollBarVisibility$
 Auto=�6A    .   �� SelectedItem 6d   �� *
PlayerRank+$ DatagridWindow����6�   �� *PlayerRanks+$ DatagridWindow��	 54        55      $
 Player��6%    65   �� *Player.Name+6W     56      $

 Team��6%    63   �� *Player.Team.LongName+6^     57      $
 Position��6%    67   �� *Player.Position+6]     58      $
 Price��6%    64   �� *Player.Price+6W     59      $
 Home PPG��6%    67   �� *HomePointsPerGame+$ N2��6p     5:      $
 Away PPG��6%    67   �� *AwayPointsPerGame+$ N2��6p     5;      $	
 PPG��6%    62   �� *PointsPerGame+$ N2��6g     5<      $
 
HomePoints��6%    69   �� *
HomePoints+$ N2��6k     5=      $
 
AwayPoints��6%    69   �� *
AwayPoints+$ N2��6k     5>      $
 HomeMinutes��6%    6:   �� *HomeMinutes+$ N2��6m     5?      $
 AwayMinutes��6%    6:   �� *AwayMinutes+$ N2��6m     5@      $
 PointsPerPound��6%    6=   �� *PointsPerPound+$ N2��6s     5A      $
 Future��6%    65   �� *FuturePoints+$ N2��6i     5B      $
 	Will Play��6%    68   �� *
WillPlay+$ N2��6h   5D        5F   
   $��2��6   $
 Auto=�6!    .  ��6D   �� *PlayerRank.Predictions+$ DatagridWindow��	 5G        5H      $
 Player��6%    65   �� *Player.Name+6W     5I      $

 Team��6%    63   �� *Player.Team.LongName+6^     5J      $
 Position��6%    67   �� *Player.Position+6]     5K      $
 Fixture��6%    66   �� *	Fixture+6T     5L      $
 Points��6%    65   �� *Points+6R   5N        5P   
   $��3��6   $
 Auto=�6!    .  ��6D   �� *PlayerRank.FormFixtures+$ DatagridWindow��	 5Q        5R      $
 Player��6%    65   �� *Player.Name+6W     5S      $

 Team��6%    63   �� *Player.Team.LongName+6^     5T      $
 Position��6%    67   �� *Player.Position+6]     5U      $
 Fixture��6%    66   �� *Match+6R     5V      $
 Minutes��6%    66   �� *MP+6O     5W      $
 Points��6%    65   �� *TP+6N   5Y        5[   
   $��3��6   $��1��6    $
 Auto=�60    .   6S   �� *
PlayerRank+$ DatagridWindow����6�   �� *Squad.Substitutes+$ DatagridWindow��	 5\        5]      $
 Player��6%    65   �� *Player.Name+6W     5^      $

 Team��6%    63   �� *Player.Team.LongName+6^     5_      $
 Position��6%    67   �� *Player.Position+6]     5`      $
 Price��6%    64   �� *Player.Price+6W     5a      $
 Home PPG��6%    67   �� *HomePointsPerGame+$ N2��6p     5b      $
 Away PPG��6%    67   �� *AwayPointsPerGame+$ N2��6p     5c      $	
 PPG��6%    62   �� *PointsPerGame+$ N2��6g     5d      $
 
HomePoints��6%    69   �� *
HomePoints+$ N2��6k     5e      $
 
AwayPoints��6%    69   �� *
AwayPoints+$ N2��6k     5f      $
 HomeMinutes��6%    6:   �� *HomeMinutes+$ N2��6m     5g      $
 AwayMinutes��6%    6:   �� *AwayMinutes+$ N2��6m     5h      $
 PointsPerPound��6%    6=   �� *PointsPerPound+$ N2��6s     5i      $
 Future��6%    65   �� *FuturePoints+$ N2��6i     5j      $
 	Will Play��6%    68   �� *
WillPlay+$ N2��6h   5l      �� 5n   
   $��2��6   $��3��6%   .+�5o         .  ��6   �� *Squad.Squad+$ DatagridWindow��	 5p      4  .System.Windows.Controls.DataGridTemplateColumn  5q      $
 Player��6-      CellTemplate 5r      �� 5s      .R�5t   "   ��  �� DisplayMemberPath$ Player.Name��6�    6+   ��  �� Mode$ OneWay=���6P   �� *RankController.PlayerRanks+$ DatagridWindow��6�   5u      5w      5y      5|      5}      5~      