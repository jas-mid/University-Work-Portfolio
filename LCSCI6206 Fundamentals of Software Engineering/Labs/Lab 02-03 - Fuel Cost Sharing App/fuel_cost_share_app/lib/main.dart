import 'package:flutter/material.dart';

void main() {
  runApp(const MyApp());
}

class MyApp extends StatelessWidget {
  const MyApp({super.key});

  // This widget is the root of your application.
  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'Fuel Cost Share',
      theme: ThemeData(colorScheme: .fromSeed(seedColor: Colors.brown)),
      home: const FuelCostSharePage(),
    );
  }
}

class FuelCostSharePage extends StatefulWidget {
  const FuelCostSharePage({super.key});

  @override
  State<FuelCostSharePage> createState() => _FuelCostSharePageState();
}

class _FuelCostSharePageState extends State<FuelCostSharePage> {
  double _fuelCost = 0;
  int _personCount = 1;
  double _tipPercent = 0;

  double get _costPerPerson =>
      _fuelCost * (1 + _tipPercent / 100) / _personCount;

  // Methods
  void increment() {
    setState(() {
      _personCount++;
    });
  }

  void decrement() {
    setState(() {
      // The cost is split between travellers, so there is always at least one.
      if (_personCount > 1) {
        _personCount--;
      }
    });
  }

  @override
  Widget build(BuildContext context) {
    final theme = Theme.of(context);
    final style = theme.textTheme.titleMedium!.copyWith(
      color: theme.colorScheme.onPrimaryContainer,
      fontWeight: FontWeight.bold,
    );
    return Scaffold(
      appBar: AppBar(title: const Text('Fuel Cost Sharing')),
      body: Padding(
        padding: const EdgeInsets.all(8.0),
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.stretch,
          children: [
            Container(
              padding: const EdgeInsets.all(18),
              decoration: BoxDecoration(
                color: theme.colorScheme.primaryContainer,
                borderRadius: BorderRadius.circular(10),
              ),
              child: Column(
                children: [
                  Text('Total Fuel Cost Per Traveller', style: style),
                  Text(
                    '£${_costPerPerson.toStringAsFixed(2)}',
                    style: style.copyWith(
                      fontSize: theme.textTheme.displaySmall?.fontSize,
                    ),
                  ),
                ],
              ),
            ),
            const SizedBox(height: 8),
            Container(
              padding: const EdgeInsets.all(8),
              decoration: BoxDecoration(
                borderRadius: BorderRadius.circular(5),
                border: Border.all(color: theme.colorScheme.primary, width: 2),
              ),
              child: Column(
                children: [
                  TextField(
                    decoration: const InputDecoration(
                      border: OutlineInputBorder(),
                      labelText: 'Enter Fuel Cost',
                      prefixText: '£',
                    ),
                    keyboardType: const TextInputType.numberWithOptions(
                      decimal: true,
                    ),
                    onChanged: (String value) {
                      setState(() {
                        _fuelCost = double.tryParse(value) ?? 0;
                      });
                    },
                  ),
                  Row(
                    mainAxisAlignment: MainAxisAlignment.spaceBetween,
                    children: [
                      Text('Split', style: theme.textTheme.titleMedium),
                      Row(
                        children: [
                          IconButton(
                            color: theme.colorScheme.primary,
                            onPressed: decrement,
                            icon: const Icon(Icons.remove),
                          ),
                          Text(
                            '$_personCount',
                            style: theme.textTheme.titleMedium,
                          ),
                          IconButton(
                            color: theme.colorScheme.primary,
                            onPressed: increment,
                            icon: const Icon(Icons.add),
                          ),
                        ],
                      ),
                    ],
                  ),
                  Row(
                    mainAxisAlignment: MainAxisAlignment.spaceBetween,
                    children: [
                      Text('Tip', style: theme.textTheme.titleMedium),
                      Text(
                        '${_tipPercent.round()}%',
                        style: theme.textTheme.titleMedium,
                      ),
                    ],
                  ),
                  Slider(
                    value: _tipPercent,
                    min: 0,
                    max: 50,
                    divisions: 10,
                    label: '${_tipPercent.round()}%',
                    onChanged: (double value) {
                      setState(() {
                        _tipPercent = value;
                      });
                    },
                  ),
                ],
              ),
            ),
          ],
        ),
      ),
    );
  }
}
