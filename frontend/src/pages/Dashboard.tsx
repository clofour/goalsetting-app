import PageTitle from '@/components/PageTitle';
import { Stack, SimpleGrid, Paper, Group, ThemeIcon, Text, Checkbox } from '@mantine/core';
import { IconStar, IconCompass, IconActivity } from '@tabler/icons-react';

export default function Dashboard() {
    const summaryCards = [
        {
            name: "North Stars",
            value: "7",
            icon: IconStar
        },
        {
            name: "Bearings",
            value: "3",
            icon: IconCompass
        },
        {
            name: "Movements",
            value: "1",
            icon: IconActivity
        }
    ]

    const movementsToday = [
        {
            "name": "uhhh lemme think",
            "description": "i forgor",
            "done": false
        },
        {
            "name": "uhhh lemme think",
            "description": "i forgor",
            "done": false
        },
        {
            "name": "uhhh lemme think",
            "description": "i forgor",
            "done": false
        }
    ]

    return (
        <Stack>
            <PageTitle name="Dashboard" description="TBD" />
            <SimpleGrid cols={{ base: 2, lg: 4 }}>
                {summaryCards.map((card) => (
                    <Paper key={card.name} p="md" withBorder>
                        <Group gap="xs" mb="xs">
                            <ThemeIcon size="sm" variant="light">
                                <card.icon size={14} />
                            </ThemeIcon>
                            <Text size="xs" fw={500} c="dimmed">{card.name}</Text>
                        </Group>
                        <Text size="xl" fw={700}>{card.value}</Text>
                    </Paper>
                ))}
            </SimpleGrid>

            {movementsToday.map((movement) => (
                <Paper key={movement.name} p="sm" withBorder>
                    <Group>
                        <Checkbox checked={movement.done} radius="xl" />
                        <Text size="sm">{movement.name}</Text>
                    </Group>
                </Paper>
            ))}
        </Stack>
    );
}