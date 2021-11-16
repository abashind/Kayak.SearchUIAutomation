﻿Feature: Search plane tickets at https://kayak.com

	As a: person who wants to travel
	In order to: buy plane tickets to the direction I need
	I want to: search the tickets using the search form
	So that: search outcome will show tickets that correspond to search criteria

	We could've used a scenario outline here but scenarios are too different for that
	Basically the search functionality itself is one of the first candidates for API testing, 
	but the test assignment specification requires to use Selenium. 

@oneway @single
Scenario: Search plane tickets in one-way direction for 1 adult person
	Given the next amount of travelers wants to fly out:
	| Type   | Amount |
	| Adults | 1      |
	And the rest of search field are filled out with the next values:
	| ClassOfService | CarryOnBags | CheckedBags | From | To  | WhenToThere | WhenBack |
	| Economy        | 0           | 0           | UFA  | MOW | 2021-11-25  | --       |
	When search button is pressed
	Then every item on the outcome first page has '1' flights
	And the flight/s have the right direction
	And every item on the outcome first page has '1' seat
	And no duplicates - not sure I need it

@roundtrip @single
Scenario: Search plane tickets for a round trip for 1 adult person
	Given the next amount of travelers wants to fly out:
	| Type   | Amount |
	| Adults | 1      |
	And the rest of search field are filled out with the next values:
	| ClassOfService | CarryOnBags | CheckedBags | From | To  | When to there | When back |
	| Economy        | 0           | 0           | UFA  | MOW | 2021-11-25    | --        |
	When search button is pressed
	Then every item on the outcome first page has '2' flights
	And the flight/s have the right direction
	And every item on the outcome first page has '1' seat
	And no duplicates - not sure I need it


@roundtrip @two
Scenario: Search plane tickets for a round trip for 2 adult person
	Given the next amount of travelers wants to fly out:
	| Type   | Amount |
	| Adults | 2      |
	And the rest of search field are filled out with the next values:
	| ClassOfService | CarryOnBags | CheckedBags | From | To  | When to there | When back  |
	| Economy        | 0           | 0           | UFA  | MOW | 2021-11-25    | 2021-11-27 |
	When search button is pressed
	Then every item on the outcome first page has '2' flights
	And the flight/s have the right direction
	And every item on the outcome first page has '2' seat
	And no duplicates - not sure I need it