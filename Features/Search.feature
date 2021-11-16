Feature: Search plane tickets at https://kayak.com

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
	| Type  | Amount |
	| Adult | 1      |
	And the rest of search field are filled out with the next values:
	| Direction | ClassOfService | CarryOnBags | CheckedBags | From | To     | WhenToThere       | WhenBack |
	| One-way   | Economy        | 0           | 0           | Ufa  | Moscow | November 19, 2021 | --       |
	When search button is pressed
	Then every item on the outcome first page has '1' flights
	And the flight has the right direction
	And every item on the outcome first page has '1' seat
	And no duplicates - not sure I need it

@roundtrip @single
Scenario: Search plane tickets for a round trip for 1 adult person
	Given the next amount of travelers wants to fly out:
	| Type  | Amount |
	| Adult | 1      |
	And the rest of search field are filled out with the next values:
	| Direction  | ClassOfService | CarryOnBags | CheckedBags | From | To     | When to there     | When back |
	| Round-trip | Economy        | 0           | 0           | Ufa  | Moscow | November 19, 2021 | --        |
	When search button is pressed
	Then every item on the outcome first page has '2' flights
	And the flights have the right directions
	And every item on the outcome first page has '1' seat
	And no duplicates - not sure I need it


@roundtrip @two
Scenario: Search plane tickets for a round trip for 2 adult person
	Given the next amount of travelers wants to fly out:
	| Type  | Amount |
	| Adult | 2      |
	And the rest of search field are filled out with the next values:
	| Direction  | ClassOfService | CarryOnBags | CheckedBags | From | To     | When to there     | When back         |
	| Round-trip | Economy        | 0           | 0           | Ufa  | Moscow | November 19, 2021 | November 20, 2021 |
	When search button is pressed
	Then every item on the outcome first page has '2' flights
	And the flights have the right directions
	And every item on the outcome first page has '2' seat
	And no duplicates - not sure I need it