import { Box, Paper, Text, Flex } from '@mantine/core';
import GoalMenu from '@/components/goals/GoalMenu';
import type { ReactElement } from 'react';
import { theme } from '@/Theme.jsx';

interface GoalCardProps {
    id: string;
    name: string;
    type: "star" | "bearing" | "movement";
    description: string;
    left: ReactElement;
    right?: ReactElement;
}

export default function GoalCard({ id, name, type, description, left, right }: GoalCardProps) {
    return (<Paper p="sm" withBorder style={{ borderLeftWidth: "2px", borderLeftStyle: "solid", borderLeftColor: theme.colors.goal[type] }}>
        <Flex align="center" gap="sm">
            {left}
            <Box flex={1}>
                <Text>{name}</Text>
                <Text size="xs" c="dimmed">{description}</Text>
            </Box>
            {right}
            <GoalMenu id={id} />
        </Flex>
    </Paper>
    )
}