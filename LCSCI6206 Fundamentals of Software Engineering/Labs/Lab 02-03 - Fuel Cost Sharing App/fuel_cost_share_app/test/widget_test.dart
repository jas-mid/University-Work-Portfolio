import 'package:flutter/material.dart';
import 'package:flutter_test/flutter_test.dart';

import 'package:fuel_cost_share_app/main.dart';

void main() {
  testWidgets('Starts at £0.00 split one way', (WidgetTester tester) async {
    await tester.pumpWidget(const MyApp());

    expect(find.text('£0.00'), findsOneWidget);
    expect(find.text('1'), findsOneWidget);
  });

  testWidgets('Splits the fuel cost between travellers', (
    WidgetTester tester,
  ) async {
    await tester.pumpWidget(const MyApp());

    await tester.enterText(find.byType(TextField), '30');
    await tester.tap(find.byIcon(Icons.add));
    await tester.tap(find.byIcon(Icons.add));
    await tester.pump();

    expect(find.text('3'), findsOneWidget);
    expect(find.text('£10.00'), findsOneWidget);
  });

  testWidgets('Split never drops below one traveller', (
    WidgetTester tester,
  ) async {
    await tester.pumpWidget(const MyApp());

    await tester.enterText(find.byType(TextField), '12.50');
    await tester.tap(find.byIcon(Icons.remove));
    await tester.pump();

    expect(find.text('1'), findsOneWidget);
    expect(find.text('£12.50'), findsOneWidget);
  });

  testWidgets('Adds the tip to each traveller\'s share', (
    WidgetTester tester,
  ) async {
    await tester.pumpWidget(const MyApp());

    await tester.enterText(find.byType(TextField), '20');
    await tester.tap(find.byIcon(Icons.add));
    // Drag the slider from 0% all the way to the 50% maximum.
    await tester.drag(find.byType(Slider), const Offset(1000, 0));
    await tester.pump();

    expect(find.text('50%'), findsWidgets);
    expect(find.text('£15.00'), findsOneWidget);
  });
}
