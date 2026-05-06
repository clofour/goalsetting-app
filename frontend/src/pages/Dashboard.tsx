import { getApiGoalStats, useGetApiGoalStats } from '@/api/endpoints/goal/goal';
import PageTitle from '@/components/shared/PageTitle';
import { Stack, SimpleGrid, Paper, Group, ThemeIcon, Text, Checkbox } from '@mantine/core';
import { IconStar, IconCompass, IconActivity } from '@tabler/icons-react';
import { useEffect } from 'react';

export default function Dashboard() {
    const { data: response, error, isLoading, mutate } = useGetApiGoalStats();
    const goalStats = response?.data;

    const summaryCards = [
        {
            name: "North Stars",
            value: goalStats?.northStarCount,
            icon: IconStar
        },
        {
            name: "Bearings",
            value: goalStats?.bearingCount,
            icon: IconCompass
        },
        {
            name: "Movements",
            value: goalStats?.movementCount,
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